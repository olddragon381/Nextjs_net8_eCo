using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Order
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task CreateOrderAsync(OrderRequestDTO order)

        {
           
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }



            var user = await _unitOfWork.Users.GetUserProfileAsync(order.UserId);

           

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            if (order.Items == null || !order.Items.Any())
            {
                throw new ArgumentException("Order must contain at least one item.");
            }
            if (!OrderValidator.IsAddressValid(order.Address))
                throw new  ValidationException("Địa chỉ không hợp lệ");

            if (!OrderValidator.IsNameValid(order.NameForOrder))
                throw new ValidationException("Tên người nhận không hợp lệ");
            if (!OrderValidator.PhoneValidate(order.Phone))
                throw new ValidationException("Sdt nhận không hợp lệ");

 


            if (order.TotalAmount <= 0)
            {
                throw new ArgumentException("Total amount must be greater than zero.");
            }   

            var data = new Domain.Entities.Order
            {
                Id = "",
                UserId = order.UserId,
               
                PaymentMethod = order.PaymentMethod,
                Items = order.Items.Select(item => new Domain.Entities.CartItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                Couponcode = order.Couponcode,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = DateTime.UtcNow
            };

            var updateprofile = new UpdateUserProfileDTO
            {
               
                NameForOrder = order.NameForOrder,
                Address = order.Address,
                Phone = order.Phone
            }
            ;

            await _unitOfWork.Users.UpdateUserProfileAsync(order.UserId,updateprofile);


            await _unitOfWork.Order.CreateAsync(data);  
        }

        public Task DeleteOrderAsync(string orderId) => _unitOfWork.Order.DeleteOrderAsync(orderId);

        public Task<List<OrderRequestDTO>> GetAllOrdersAsync()
        {


            return null;

        }

        public Task<OrderRequestDTO> GetOrderByIdAsync(string orderId)
        {
            var order = _unitOfWork.Order.GetOrderByIdAsync(orderId);
            return null; 
        }

        public async Task<List<GetOrderDTO>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _unitOfWork.Order.GetOrdersByUserIdAsync(userId);
            var profile = await _unitOfWork.Users.GetUserProfileAsync(userId);


            var orderDtos = new List<GetOrderDTO>();

            foreach (var order in orders)
            {
                var itemDtos = new List<CartItemDTO>();

                foreach (var item in order.Items)
                {
                    if (string.IsNullOrEmpty(item.BookId))
                    {
                        continue;
                    }

                    // Lấy thông tin sách từ BookRepository
                    var book = await _unitOfWork.Book.GetByIdAsync(item.BookId);

                    itemDtos.Add(new CartItemDTO
                    {
                        Id = item.BookId,
                        Title = book?.Title ?? "Unknown",
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Image = book?.Image,
                        
                    });
                }

                var orderDto = new GetOrderDTO
                {
                    Id = order.Id,
                    Address = profile.Profile.Address,
                    Phone = profile.Profile.Phone,
                    PaymentMethod = order.PaymentMethod,
                    Note = order.Note,
                    Couponcode = order.Couponcode,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,
                    Items = itemDtos
                };

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }


        public Task UpdateOrderAsync(OrderRequestDTO order)
        {
            throw new NotImplementedException();
        }
    }
}
