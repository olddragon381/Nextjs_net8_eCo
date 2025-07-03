using BookstoreApp.Application.DTOs;
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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IMongoCollection<Discount> _discountCollection;

        public DiscountRepository(IMongoDatabase database)
        {
            // Ensure the database parameter is not null.
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _discountCollection = database.GetCollection<Discount>("Discounts");
        }

        public async Task<bool> ApplyDiscountToCartAsync(string cartId, string discountCode)
        {
            await GetDiscountByCodeAsync(discountCode);
            return true;
        }

        public Task<IEnumerable<DiscountDTO>> GetAllActiveDiscountsAsync()
        {
            var discounts = _discountCollection
                .Find(d => d.IsActive)
                .ToListAsync();
            return discounts.ContinueWith(task => task.Result.Select(d => new DiscountDTO
            {
                Code = d.CouponCode,
                Percentage = d.Percentage,
                FixedAmount = d.FixedAmount,
                IsActive = d.IsActive
            }));
        }

        public async Task<DiscountDTO?> GetDiscountByCodeAsync(string code)
        {
            var discount = await _discountCollection
                .Find(d => d.CouponCode == code && d.IsActive)
                .FirstOrDefaultAsync();

            if (discount == null)
            {
                return null;
            }

            return new DiscountDTO
            {
                Code = discount.CouponCode,
                Percentage = discount.Percentage,
                FixedAmount = discount.FixedAmount,
                IsActive = discount.IsActive
            };
        }

        public Task<bool> RemoveDiscountFromCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }
    }
}
