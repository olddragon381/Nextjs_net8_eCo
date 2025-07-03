using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class UserInfoDTO
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public Role UserRole { get; set; }  = Role.User; 
    }
}
