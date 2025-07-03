using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class GetOrderDTO
    {
        public required string Id  { get; set; }

        public string Address { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "Cash on Delivery";
        public string Phone { get; set; } = string.Empty;
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

        public string? Couponcode { get; set; }

        public string? Note { get; set; }
        public decimal TotalAmount { get; set; } = 0.0m;


        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
