using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Admin
{
    public class AdminService : IAdminServicecs
    {
        private readonly IUnitOfWork _unitOfWork;


        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task CreateBookAsync(CreateBookDTO createBookDTO)
        {
            await _unitOfWork.Book.CreateAsync(new CreateBookDTO
            {
                Title = createBookDTO.Title,
                Image = createBookDTO.Image,
                Authors = createBookDTO.Authors,
                Description = createBookDTO.Description,
                Rating = createBookDTO.Rating,
                Status = createBookDTO.Status,
                RatingCount = createBookDTO.RatingCount,
                ReviewCount = createBookDTO.ReviewCount,
                NumPages = createBookDTO.NumPages,
                Price = createBookDTO.Price,
                Genres = createBookDTO.Genres
            });
        }

        public async Task DeleteBookAsync(string bookId)
        {
            await _unitOfWork.Book.DeleteAsync(bookId);
        }

        public Task DeleteOrderAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(string userId) => await _unitOfWork.Users.DeleteAsync(userId);



      

        public async Task<PaginationDTO<OrderDetailsDTO>> GetOrderPagingAsync(int page, int pageSize)
        {
            var (orders, total) = await _unitOfWork.Order.GetOrderPagingAsync(page, pageSize);

          

            var orderDTOs = orders.Select(o => new OrderDetailsDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                PaymentMethod = o.PaymentMethod,
                Items = o.Items,
                Couponcode = o.Couponcode,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                Note = o.Note,
                CreatedAt = o.CreatedAt
            }).ToList();

            return new PaginationDTO<OrderDetailsDTO>
            {
                Items = orderDTOs,
                TotalItems = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<int> GetTotalBookCountAsync()
        {
            return await _unitOfWork.Book.GetTotalBooksCountAsync();
        }

        public Task<int> GetTotalOrderCountAsync()
        {
            
            return _unitOfWork.Order.GetTotalOrdersCountAsync();
        }

        public Task<int> GetTotalUsersCountAsync() => _unitOfWork.Users.GetTotalUsersAsync();  

        public async Task<PaginationDTO<UserDetailsDTO>> GetUserPagingAsync(int page, int pageSize)
        {
            var (users, total) = await _unitOfWork.Users.GetUserPagingAsync(page, pageSize);
            var user = users.Select(b => new UserDetailsDTO
            {
                Id = b.Id,
                FullName = b.FullName,
                Email = b.Email,
                UserRole = b.UserRole,
                CreatedAt = b.CreatedAt,
                NameForOrder = b.NameForOrder,
                Phone = b.Phone,
                Address = b.Address,
                PasswordHash = b.PasswordHash,
                Salt = b.Salt

            }).ToList();
            return new PaginationDTO<UserDetailsDTO>
            {
                Items = user,
                TotalItems = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task UpdateBookAsync(string bookId, UpdateBookDTO updateBookDTO)
        {
          await   _unitOfWork.Book.UpdateAsync(bookId, updateBookDTO);
        }

        public async Task UpdateOrderStatusAsync(string orderId, OrderStatus newStatus)
        {
            await _unitOfWork.Order.UpdateOrderStatusAsync(orderId, newStatus);
        }

        public async Task UpdateUserRoleAsync(string userId, Role newRole)
        {
            await _unitOfWork.Users.UpdateUserRoleAsync(userId, newRole);
    
        }

       
    }
}
