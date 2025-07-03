using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository

    {
        private readonly IMongoCollection<Order> _orderCollection;


        public OrderRepository(IMongoDatabase database)
        {
            // Ensure the database parameter is not null.
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }
            _orderCollection = database.GetCollection<Order>("Orders");
        }

        public async Task CreateAsync(Order order)
        {
           await _orderCollection.InsertOneAsync(order);
        }

        public Task DeleteOrderAsync(string orderId)
        {
            GetOrderByIdAsync(orderId).Wait(); 
            return _orderCollection.DeleteOneAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync() =>  await _orderCollection.Find(_ => true).ToListAsync();
        

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
           var result =  await _orderCollection.Find(o => o.Id == orderId).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            return result;
        }

        public Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {


            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            return _orderCollection.Find(o => o.UserId == userId).ToListAsync();

        }

        public Task<bool> OrderExistsAsync(string orderId)
        {
            return _orderCollection.Find(o => o.Id == orderId).AnyAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var findorder = await GetOrderByIdAsync(order.Id); // Ensure the order exists before updating
            if (findorder == null)
            {
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");
            }
            var updateDefinition = Builders<Order>.Update
                .Set(o => o.Status, order.Status)
                .Set(o => o.TotalAmount, order.TotalAmount)
                .Set(o => o.CreatedAt, order.CreatedAt)
                .Set(o => o.Items, order.Items)

                .Set(o => o.Couponcode, order.Couponcode)
            

                .Set(o => o.PaymentMethod, order.PaymentMethod);
            var updateResult = await _orderCollection.UpdateOneAsync(i => i.Id == order.Id, updateDefinition);
        }
    }
}
