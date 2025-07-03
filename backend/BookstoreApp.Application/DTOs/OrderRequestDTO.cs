using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class OrderRequestDTO
    {
        
        public required string UserId { get; set; }
        public string NameForOrder { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "Cash on Delivery";

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public string? Couponcode { get; set; }

        public string? Note { get; set; }
        public decimal TotalAmount { get; set; } = 0.0m;


        public OrderStatus Status { get; set; } = OrderStatus.Pending;

    }
}
