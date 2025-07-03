using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class CartItem
    {

        public string? BookId { get; set; }
        public decimal Price { get; set; } 
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }

}
