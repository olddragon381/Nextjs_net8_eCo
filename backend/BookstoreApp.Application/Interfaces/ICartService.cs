using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface ICartService 
    {
        Task AddToCartAsync(string userId, List<AddToCartRequestDTO> request);
        
        Task UpdateCartItemAsync(string userId,string bookId, int quantity);
        Task AddAsync(string userId, string bookId, int quantity);

        Task RemoveFromCartAsync(string userId, string bookId);
        Task RemoveItemFromCartAsync(string userId, string bookId);
        Task<CartDTO> GetCartByUserIdAsync(string userId);

      
        Task ClearCartAsync(string userId);

        /// <summary>
        /// Áp dụng mã giảm giá (nếu có logic discount).
        /// </summary>
        Task ApplyDiscountAsync(string userId, string discountCode);
    }
}
