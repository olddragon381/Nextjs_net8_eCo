using BookstoreApp.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("fullName")]
        [BsonRequired]
        public required string FullName { get; set; }
        [BsonElement("email")]
        [BsonRequired]
        public required string Email { get; set; }
        [BsonElement("passwordHash")]
        [BsonRequired]
        public required  string PasswordHash { get; set; }
        [BsonElement("salt")]
        [BsonRequired]
        public required  string Salt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Role UserRole { get; set; } = Role.User;



        //User Profile Information
        [BsonElement("nameforoder")]
        public string NameForOrder { get; set; } = string.Empty;

        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;
        [BsonElement("address")]
        public string Address { get; set; } = string.Empty;




    }
}
