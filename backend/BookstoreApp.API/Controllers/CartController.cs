using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        
        [Authorize]
        [HttpPost("cart")]
        public async Task<IActionResult> AddToCart([FromBody] List<AddToCartRequestDTO> request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            await _cartService.AddToCartAsync(userId, request );
            return Ok(new { message = "Book added to cart successfully" });
        }

        [Authorize]
        [HttpDelete("delete-item/{bookId}")]
        public async Task<IActionResult> RemoveItem(string bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _cartService.RemoveItemFromCartAsync(userId, bookId);
            return Ok(new { message = "Item removed from cart" });
        }
        [Authorize]
        [HttpPost("checkDiscountCode")]
        public async Task<IActionResult> CheckDiscountCode(string code)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            await _cartService.ApplyDiscountAsync(userId, code);
            return Ok(new { message = "ok Code discount" });
        }



        [Authorize]
        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCartAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            await _cartService.ClearCartAsync(userId);
            return Ok(new { message = "Ok Clear" });
        }
        [Authorize]
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCartAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return NotFound(new { message = "Cart not found" });
            }

            return Ok(cart); 
        }


        [Authorize]
        [HttpPut("cart")]
        public async Task<IActionResult> UpdateCartItemAsync([FromBody] AddToCartRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            await _cartService.UpdateCartItemAsync(userId, request.BookId, request.Quantity);

            return Ok(new { message = "Cart item updated successfully" });
        }
        
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(List<AddToCartRequestDTO> requestList)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User not authenticated" });

            foreach (var request in requestList)
            {
                await _cartService.AddAsync(userId, request.BookId, request.Quantity);
            }

            return Ok(new { message = "Items added to cart" });
        }

    }
}
