using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class DiscountDTO
    {
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public decimal FixedAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
