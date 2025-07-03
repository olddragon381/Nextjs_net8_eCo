using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class CartItemDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
