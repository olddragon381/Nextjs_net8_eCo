using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class RegisterDTO
    {
        public required string Fullname { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

        public Role Role { get; set; } = Role.User; 
    }
}
