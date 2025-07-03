using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Cart
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AddToCartAsync(string userId, List<AddToCartRequestDTO> requests)
        {

            var user = await _unitOfWork.Users.GetByIdOneUserAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    Items = new List<CartItem>(),
                    UserId = userId,
                    LastUpdated = DateTime.UtcNow
                };
            }

            foreach (var request in requests)
            {
                if (request.Quantity <= 0)
                    throw new ArgumentException("Quantity must be greater than zero");

                var book = await _unitOfWork.Book.GetByIdAsync(request.BookId);
                if (book == null)
                    throw new KeyNotFoundException($"Book with ID '{request.BookId}' not found.");

                var existingItem = cart.Items.FirstOrDefault(i => i.BookId == request.BookId);
                if (existingItem != null)
                {
                    existingItem.Quantity += request.Quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        BookId = request.BookId,
                        Quantity = request.Quantity,
                        Price = book.Price
                    });
                }
            }

            cart.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.Cart.SaveAsync(cart);
        }

        public async Task ApplyDiscountAsync(string userId, string discountCode)
        {
            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }
            var discount = await _unitOfWork.Discount.GetDiscountByCodeAsync(discountCode);
        }

        public async Task ClearCartAsync(string userId)
        {

            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null) return;

            cart.Items.Clear();
      
            cart.AppliedDiscount = null;
            cart.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.Cart.SaveAsync(cart);




        }

        public async Task<CartDTO> GetCartByUserIdAsync(string userId)
        {
            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);

            // Nếu chưa có cart thì tạo cart mới trống
            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>(),
              
                    AppliedDiscount = null
                };

                await _unitOfWork.Cart.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            // Duyệt items
            var cartItemDtos = new List<CartItemDTO>();
            if (cart.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    if (item == null || string.IsNullOrEmpty(item.BookId))
                        continue;

                    var book = await _unitOfWork.Book.GetByIdAsync(item.BookId);
                    if (book == null) continue;

                    cartItemDtos.Add(new CartItemDTO
                    {
                        Id = item.BookId,
                        Title = book.Title,
                        Image = book.Image,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }
            }

            return new CartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = cartItemDtos,
                
                AppliedDiscount = cart.AppliedDiscount == null ? null : new DiscountDTO
                {
                    Code = cart.AppliedDiscount.CouponCode,
                    Percentage = cart.AppliedDiscount.Percentage,
                    FixedAmount = cart.AppliedDiscount.FixedAmount,
                    IsActive = cart.AppliedDiscount.IsActive
                }
            };
        }
        public async Task RemoveItemFromCartAsync(string userId, string bookId)
        {
            await _unitOfWork.Cart.RemoveItemAsync(userId, bookId);
        }

        public async Task RemoveFromCartAsync(string userId, string bookId)
        {
            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found.");
            }

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null)
            {
                throw new Exception("Book not found in cart.");
            }

            cart.Items.Remove(item);
            cart.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.Cart.SaveAsync(cart);

        }

        public async Task UpdateCartItemAsync(string userId, string bookId, int quantity)
        {
            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null)
                throw new KeyNotFoundException("Cart not found for the user");

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null)
                throw new KeyNotFoundException($"Item with Book ID '{bookId}' not found in the cart");

            if (quantity <= 0)
            {
                cart.Items.Remove(item); // Xoá nếu quantity không hợp lệ
            }
            else
            {
                item.Quantity = quantity;
            }

            cart.LastUpdated = DateTime.UtcNow;
            await _unitOfWork.Cart.SaveAsync(cart);
        }

        public async Task AddAsync(string userId, string bookId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            var user = await _unitOfWork.Users.GetByIdOneUserAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var book = await _unitOfWork.Book.GetByIdAsync(bookId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID '{bookId}' not found.");

            var cart = await _unitOfWork.Cart.GetByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>(),
                    LastUpdated = DateTime.UtcNow
                };
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity; 
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    Price = book.Price
                });
            }

            cart.LastUpdated = DateTime.UtcNow;
            await _unitOfWork.Cart.SaveAsync(cart);
        }

    }
}
