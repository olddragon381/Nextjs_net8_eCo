using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class AddToCartRequestDTO
    {
        public required string BookId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
