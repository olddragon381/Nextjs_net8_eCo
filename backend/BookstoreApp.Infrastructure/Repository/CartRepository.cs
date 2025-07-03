using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        public CartRepository(IMongoDatabase database)
        {
            // Ensure the database parameter is not null.
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _cartCollection = database.GetCollection<Cart>("Carts");
        }

        public async Task AddAsync(Cart cart)
        {
            await _cartCollection.InsertOneAsync(cart); 
        }

        public async Task ClearCartAsync(string userId)
        {
           await _cartCollection.DeleteManyAsync(c => c.UserId == userId);
        }

        public Task DeleteAsync(string cartId)
        {
           return _cartCollection.DeleteOneAsync(c => c.Id == cartId); 
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
            return await _cartCollection.Find(filter).AnyAsync();
        }

        public async Task<Cart?> GetByIdAsync(string cartId)
        {
            if (!ObjectId.TryParse(cartId, out var objectId))
                throw new ArgumentException("Invalid cart ID");

            // Fix: Use the correct type for the filter field
            var filter = Builders<Cart>.Filter.Eq(c => c.Id, cartId);
            return await _cartCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _cartCollection.Find(c => c.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task RemoveItemAsync(string userId, string bookId)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.UserId, userId);
            var update = Builders<Cart>.Update.PullFilter(c => c.Items, i => i.BookId == bookId);
            await _cartCollection.UpdateOneAsync(filter, update);
        }

        public async Task SaveAsync(Cart cart)
        {
            {
                var filter = Builders<Cart>.Filter.Eq(c => c.UserId, cart.UserId);
                var existing = await GetByUserIdAsync(cart.UserId);
                if (existing == null)
                {
                    await _cartCollection.InsertOneAsync(cart);
                }
                else
                {
                    cart.Id = existing.Id; // giữ lại Id để Replace
                    await _cartCollection.ReplaceOneAsync(filter, cart);
                }
            }
        }

    
    }
}
