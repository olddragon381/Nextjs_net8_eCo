using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Order
{
    public static class OrderValidator
    {
        public static bool IsAddressValid(string address)
            => !string.IsNullOrWhiteSpace(address) && address.Length >= 2;

        public static bool IsNameValid(string name)
            => !string.IsNullOrWhiteSpace(name) && name.Length >= 2;

        public static bool PhoneValidate(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Regex cho số điện thoại Việt Nam: bắt đầu bằng 0, theo sau là 9 hoặc 10 chữ số
            var regex = new System.Text.RegularExpressions.Regex(@"^0\d{9}$");
            return regex.IsMatch(phone);
        }
    }
}
