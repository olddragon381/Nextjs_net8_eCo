using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);

        Task<Order> GetOrderByIdAsync(string orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);
        Task<bool> OrderExistsAsync(string orderId);

    }
}
