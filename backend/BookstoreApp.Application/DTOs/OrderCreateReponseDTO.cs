using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class OrderCreateReponseDTO
    {
        public required string OrderId { get; set; }


        public decimal TotalAmount { get; set; } = 0.0m;



    }
}
