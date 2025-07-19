using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    internal class Vnpaypayment
    {
        public class PaymentInformationModel
        {
            public required string OrderType { get; set; }
            public double Amount { get; set; }
            public required string OrderDescription { get; set; }
            public required string Name { get; set; }
        }
        public class PaymentResponseModel
        {
            public required string OrderDescription { get; set; }
            public required string TransactionId { get; set; }
            public required string OrderId { get; set; }
            public required string PaymentMethod { get; set; }
            public required string PaymentId { get; set; }
            public bool Success { get; set; }
            public required string Token { get; set; }
            public required string VnPayResponseCode { get; set; }
        }


    }
}
