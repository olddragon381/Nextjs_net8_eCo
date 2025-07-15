using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IAdminServicecs
    {
        Task<PaginationDTO<UserDetailsDTO>>  GetUserPagingAsync(int page, int pageSize);

        Task DeleteUserAsync(string userId);
        Task UpdateUserRoleAsync(string userId, Role newRole);

        Task<int> GetTotalUsersCountAsync();

        Task<int> GetTotalOrderCountAsync();




        Task<PaginationDTO<OrderDetailsDTO>> GetOrderPagingAsync(int page, int pageSize);
        
        Task UpdateOrderStatusAsync(string orderId, OrderStatus newStatus);
        Task DeleteOrderAsync(string orderId);




        Task<int> GetTotalBookCountAsync();
        Task CreateBookAsync(CreateBookDTO createBookDTO);
        Task UpdateBookAsync(string bookId, UpdateBookDTO updateBookDTO);

        Task DeleteBookAsync(string bookId);
    }
}
