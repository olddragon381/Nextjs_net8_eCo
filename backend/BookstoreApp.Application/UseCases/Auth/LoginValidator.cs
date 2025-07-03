using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Auth
{
    public static class LoginValidator
    {
        public static void Validate(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.PasswordHash))
                
            {
                throw new ValidationException("Thông tin đăng nhập không hợp lệ");
            }

            if (!dto.Email.Contains('@'))
            {
                throw new ValidationException("Email không hợp lệ");
            }

            if (dto.PasswordHash.Length < 2)
            {
                throw new ValidationException("Mật khẩu phải có ít nhất 2 ký tự");
            }
        }
    }
}
