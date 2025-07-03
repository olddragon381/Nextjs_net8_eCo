using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class UserProfileDTO
    {

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;


        public UpdateUserProfileDTO Profile { get; set; } = new UpdateUserProfileDTO();

    }
    public class UpdateFullnameRequest
    {
        public string? FullName { get; set; }
    }
}
