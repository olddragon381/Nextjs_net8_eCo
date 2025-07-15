using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task CreateAsync(User user)
        => await _userCollection.InsertOneAsync(user);

        public async Task DeleteAsync(string id)
        => await _userCollection.DeleteOneAsync(u => u.Id == id);

        public async Task<List<User>> GetAllAsync()
        => await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetByEmailAsync(string email)
        => await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<List<User>> GetListByIdAsync(List<string> ids)
        {

            var users = await _userCollection.AsQueryable()
                                             .Where(u => ids.Contains(u.Id))
                                             .ToListAsync();

            return users;
        }

        

        public async Task UpdateAsync(User user) =>
            await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);


        
        public async Task<List<UserInfoDTO>> GetListUsersByIdsAsync(List<string> userIds)
        {
            var filter = Builders<User>.Filter.In(u => u.Id, userIds);
            var users = await _userCollection.Find(filter).ToListAsync();

            var userDtos = users.Select(u => new UserInfoDTO
            {
                FullName = u.FullName,
                Email = u.Email,
                UserRole = u.UserRole
            }).ToList();
            return userDtos;
        }

        public async Task<UserInfoDTO> GetByIdOneUserAsync(string id)
        {
            var user = await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("ko co user");
            }

            return new UserInfoDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }
        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("ko co user");
            }

            return new UserProfileDTO
            {
                

                Email = user.Email,
                FullName = user.FullName,
                Profile = {Address = user.Address, Phone = user.Phone , NameForOrder = user.NameForOrder, },
                

            };
        }


        public async Task<UpdateUserProfileDTO> UpdateUserProfileAsync(string userid, UpdateUserProfileDTO userProfile)
        {
            var user = await _userCollection.Find(u => u.Id == userid).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            // Cập nhật thông tin
           
            user.Address = userProfile.Address ?? user.Address;
            user.Phone = userProfile.Phone ?? user.Phone;
            user.NameForOrder = userProfile.NameForOrder ?? user.NameForOrder;

            await _userCollection.ReplaceOneAsync(u => u.Id == userid, user);

            // Return the updated user profile
            return new UpdateUserProfileDTO 
            {
               
                Address = user.Address,
                Phone = user.Phone,
                NameForOrder = user.NameForOrder
            };
        }


        public async Task UpdateUserFullNameAsync(string userid, string fullname)
        {
            var user = await _userCollection.Find(u => u.Id == userid).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            // Update the user's full name
            user.FullName = fullname;

            await _userCollection.ReplaceOneAsync(u => u.Id == userid, user);
        }

        public Task<bool> IsAdminAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId) &
                          Builders<User>.Filter.Eq(u => u.UserRole, Domain.Enums.Role.Admin);
            return _userCollection.Find(filter).AnyAsync();
        }

        public Task<bool> IsSuperAdminAsync(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId) &
                         Builders<User>.Filter.Eq(u => u.UserRole, Domain.Enums.Role.SuperAdmin);
            return _userCollection.Find(filter).AnyAsync();
        }

        public Task ChangePasswordAsync(string userid, string changePassword, string salt)
        {
            var use = _userCollection.Find(u => u.Id == userid).FirstOrDefaultAsync();
            if (use == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }
            var update = Builders<User>.Update
                .Set(u => u.PasswordHash, changePassword)
                .Set(u => u.Salt, salt);
            return _userCollection.UpdateOneAsync(u => u.Id == userid, update);
        }

        public async Task<bool> CheckUserInDatabaseAsync(string userId)
        {
            return await _userCollection.Find(u => u.Id == userId).AnyAsync();
        }

        public async Task<(List<User> users, int total)> GetUserPagingAsync(int page, int pageSize)
        {
            var total = (int)await _userCollection.CountDocumentsAsync(FilterDefinition<User>.Empty);

            var users = await _userCollection.AsQueryable()
                                             .Skip((page - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

            return (users, total);
        }

        public async Task<int> GetTotalUsersAsync()
        {
           return (int)await _userCollection.CountDocumentsAsync(FilterDefinition<User>.Empty);
        }

        public async Task UpdateUserRoleAsync(string userId, Role newRole)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            var update = Builders<User>.Update.Set(u => u.UserRole, newRole);
            await _userCollection.UpdateOneAsync(u => u.Id == userId, update);
        }
    }
}

