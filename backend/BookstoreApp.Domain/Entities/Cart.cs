using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        public required string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();
        public Discount? AppliedDiscount { get; set; } 
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice
        {
            get
            {
                var total = Items.Sum(i => i.Total);
                
                var discountPercentage = AppliedDiscount?.Percentage ?? 0;
                var discountFixedAmount = AppliedDiscount?.FixedAmount ?? 0;
                if (AppliedDiscount == null || !AppliedDiscount.IsActive)
                {
                    return total;
                }
                return Math.Max(0, total - discountPercentage - discountFixedAmount);
            }
        }
    }

}
