using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
