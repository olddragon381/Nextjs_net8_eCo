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
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);

        Task<Order> GetOrderByIdAsync(string orderId);
        Task<int> GetTotalOrdersCountAsync();
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);

        Task UpdateOrderStatusAsync(string orderId, OrderStatus newStatus);
        Task UpdatePaymentStatusAsync(string orderId, string status);
        Task<(List<Order> orders, int total)> GetOrderPagingAsync(int page, int pageSize);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);
        Task<bool> OrderExistsAsync(string orderId);

    }
}
