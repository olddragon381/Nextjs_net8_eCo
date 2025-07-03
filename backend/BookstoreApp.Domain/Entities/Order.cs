using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Domain.Enums;

namespace BookstoreApp.Domain.Entities
{
    public class Order
    {

        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("userid")]
        public required string UserId { get; set; }
   
        [BsonElement("paymentmethod")]
        public required string PaymentMethod { get; set; }

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        [BsonElement("couponcode")]
        public string? Couponcode { get; set; }

        [BsonElement("totalamount")]
        public decimal TotalAmount { get; set; } = 0.0m;
        [BsonElement("status")]

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [BsonElement("note")]

        public string Note { get; set; } = string.Empty;

        [BsonElement("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}