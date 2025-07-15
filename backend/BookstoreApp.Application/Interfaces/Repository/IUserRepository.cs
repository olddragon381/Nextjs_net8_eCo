using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface IUserRepository
    {

        Task<(List<User> users, int total)> GetUserPagingAsync(int page, int pageSize);
        Task<int> GetTotalUsersAsync();
        Task UpdateUserRoleAsync(string userId, Role newRole);

        Task<bool> CheckUserInDatabaseAsync(string userId);

        Task<bool> IsAdminAsync(string userId);
        Task<bool> IsSuperAdminAsync(string userId);
        Task<User?> GetByEmailAsync(string email);
        

        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);


        Task ChangePasswordAsync(string userid, string changePassword, string salt);

        Task<List<UserInfoDTO>> GetListUsersByIdsAsync(List<string> userIds);
        Task<UserInfoDTO> GetByIdOneUserAsync(string id);


        Task<UserProfileDTO> GetUserProfileAsync(string userId);
        Task<UpdateUserProfileDTO> UpdateUserProfileAsync(string userid, UpdateUserProfileDTO userInfo);
        Task UpdateUserFullNameAsync(string userid, string fullname);
    }
}
