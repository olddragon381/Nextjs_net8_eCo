using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class Discount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        public string? CouponCode { get; set; }

        public decimal Percentage { get; set; } = 0;      // 10% -> 0.1
        public decimal FixedAmount { get; set; } = 0;     // 50.000 VNĐ

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public int? UsageLimit { get; set; } // Tổng số lần dùng
        public int TimesUsed { get; set; } = 0;

        public bool IsActive => DateTime.UtcNow >= ValidFrom &&
                                DateTime.UtcNow <= ValidTo &&
                                (UsageLimit == null || TimesUsed < UsageLimit);
    }
}
