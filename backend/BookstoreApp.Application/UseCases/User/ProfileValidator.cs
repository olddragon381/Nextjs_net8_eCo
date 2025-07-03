using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.User
{
    public static class ProfileValidator
    {
        public static bool PhoneValidate(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Regex cho số điện thoại Việt Nam: bắt đầu bằng 0, theo sau là 9 hoặc 10 chữ số
            var regex = new System.Text.RegularExpressions.Regex(@"^(0)(3[2-9]|5[689]|7[06-9]|8[1-5]|9[0-9])[0-9]{7}$");
            return regex.IsMatch(phone);
        }
        public static bool AddressValidate(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            // Địa chỉ hợp lệ: cho phép chữ cái, số, dấu chấm, phẩy, gạch ngang, gạch dưới, khoảng trắng, không có ký tự lạ.
            var regex = new System.Text.RegularExpressions.Regex(@"^[\w\s\.,\-\/]+$");

            // Độ dài tối thiểu và tối đa (ví dụ 5–200 ký tự)
            return regex.IsMatch(address) && address.Length >= 5 && address.Length <= 200;
        }
    }
}

