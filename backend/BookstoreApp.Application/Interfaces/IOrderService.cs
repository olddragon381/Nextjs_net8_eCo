using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderRequestDTO order);
        Task<OrderRequestDTO> GetOrderByIdAsync(string orderId);
        Task<List<OrderRequestDTO>> GetAllOrdersAsync();
        Task<List<GetOrderDTO>> GetOrdersByUserIdAsync(string userId);
        Task DeleteOrderAsync(string orderId);
        Task UpdateOrderAsync(OrderRequestDTO order);
    }
}
