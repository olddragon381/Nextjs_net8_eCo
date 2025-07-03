using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class AuthResponeDTO
    {
        public required string UserId { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
