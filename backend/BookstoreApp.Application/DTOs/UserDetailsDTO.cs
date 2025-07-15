using BookstoreApp.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class UserDetailsDTO
    {

        public string Id { get; set; } = string.Empty;

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required string Salt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Role UserRole { get; set; } = Role.User;
        public string NameForOrder { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
