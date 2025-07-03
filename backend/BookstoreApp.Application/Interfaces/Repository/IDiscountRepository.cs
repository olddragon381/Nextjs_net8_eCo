using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface IDiscountRepository
    {
        // Define methods for discount repository
        // For example:
        Task<DiscountDTO?> GetDiscountByCodeAsync(string code);
        Task<IEnumerable<DiscountDTO>> GetAllActiveDiscountsAsync();
        Task<bool> ApplyDiscountToCartAsync(string cartId, string discountCode);
        Task<bool> RemoveDiscountFromCartAsync(string cartId);
    }
}
