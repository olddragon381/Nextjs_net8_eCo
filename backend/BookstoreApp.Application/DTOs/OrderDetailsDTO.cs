using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class OrderDetailsDTO
    {

        public string Id { get; set; } = string.Empty;
        public required string UserId { get; set; }

        public required string PaymentMethod { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public string? Couponcode { get; set; }

        public decimal TotalAmount { get; set; } = 0.0m;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string Note { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

 

}

