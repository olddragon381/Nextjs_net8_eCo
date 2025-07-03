using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.User
{
    public class UserProfileService : IUserProfileService
    {

        private readonly IUnitOfWork _unitOfWork;


        public UserProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            
            var userProfile = await _unitOfWork.Users.GetUserProfileAsync(userId);

            return userProfile;
          
        }

        public async Task UpdateUserFullNameAsync(string userid, string fullname)
        {
            await _unitOfWork.Users.UpdateUserFullNameAsync(userid, fullname);
        }

        public async Task<UpdateUserProfileDTO> UpdateUserProfileAsync(string userid, UpdateUserProfileDTO request)
        {
            ProfileValidator.PhoneValidate
                (request.Phone);
            ProfileValidator.AddressValidate(request.Address);
            if (string.IsNullOrEmpty(userid))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(request));
            }
            if (ProfileValidator.PhoneValidate(request.Phone) == false)
            {
                throw new ArgumentException("Invalid phone number format.", nameof(request.Phone));
            }
            if (ProfileValidator.AddressValidate(request.Address) == false)
            {
                throw new ArgumentException("Invalid address format.", nameof(request.Address));
            }
            var userProfile = await _unitOfWork.Users.UpdateUserProfileAsync(userid, request);

            if (userProfile != null)
            {
                return userProfile;
            }

            throw new Exception("User profile not found or could not be updated.");
        }
    }
}
