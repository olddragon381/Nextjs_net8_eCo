using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task SaveAsync(Cart cart);
        Task<Cart?> GetByIdAsync(string cartId);
        Task<Cart?> GetByUserIdAsync(string userId);
        Task AddAsync(Cart cart);
        Task DeleteAsync(string cartId);
        Task<bool> ExistsAsync(string userId);
        Task RemoveItemAsync(string userId, string bookId);
        Task ClearCartAsync(string userId);
    }
}
