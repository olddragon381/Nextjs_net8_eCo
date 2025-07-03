using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Auth
{
    public static class RegisterValidator
    {
        public static void Validate(RegisterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Fullname))
            {
                throw new ValidationException("Thông tin đăng ký không hợp lệ");
            }

            if (!dto.Email.Contains('@'))
            {
                throw new ValidationException("Email không hợp lệ");
            }

            if (dto.Password.Length < 2)
            {
                throw new ValidationException("Mật khẩu phải có ít nhất 2 ký tự");
            }
        }
    }
}
