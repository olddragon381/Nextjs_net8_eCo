using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class CartDTO
    {
        public string? Id { get; set; }
        public required string UserId { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();
     
        public DiscountDTO? AppliedDiscount { get; set; }
    }
}
