using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IUserProfileService
    {

        Task<UpdateUserProfileDTO> UpdateUserProfileAsync(string userid, UpdateUserProfileDTO request);
        Task<UserProfileDTO> GetUserProfileAsync(string userId);
        Task  UpdateUserFullNameAsync(string userid, string fullname);
    }
}
