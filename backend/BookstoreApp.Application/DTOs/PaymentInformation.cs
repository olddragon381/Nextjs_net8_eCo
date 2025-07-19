using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class PaymentInformation
    {

        public required string OrderType { get; set; }
        public double Amount { get; set; }
        public required string OrderDescription { get; set; }
        public required string Name { get; set; }
    }
}
