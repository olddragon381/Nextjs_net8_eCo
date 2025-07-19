using BookstoreApp.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static BookstoreApp.Domain.Entities.Vnpaypayment;

namespace BookstoreApp.Application.Interfaces
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformation model, string ipAddress);
        PaymentResponse PaymentExecute(IQueryCollection collections);

    }
}
