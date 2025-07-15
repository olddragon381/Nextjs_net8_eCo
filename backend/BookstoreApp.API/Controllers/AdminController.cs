using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Auth;
using BookstoreApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IAdminServicecs _adminService;
        public AdminController( ICategoryService categoryService, IBookService bookService, IAdminServicecs adminService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
            _adminService = adminService;
        }

        [HttpGet("user/paging")]
        public async Task<IActionResult> GetUser([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {


        

            var result = await _adminService.GetUserPagingAsync(page, pageSize);
            return Ok(result);
        }
        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUsers([FromBody] string userId)
        {
            await _adminService.DeleteUserAsync(userId); // Removed assignment to a variable
            return Ok("User deleted successfully."); // Added a success message
        }
        [HttpGet("getcountusers")]
        public async Task<IActionResult> GetCountUser()
        {
            var userscount = await _adminService.GetTotalUsersCountAsync();
            return Ok(userscount);
        }

        [HttpPost("changerole")]
        public async Task<IActionResult> ChangeRoleUser([FromBody] string userId, Role newRole)
        {
            await _adminService.UpdateUserRoleAsync(userId,newRole);
            return Ok("User Change Role successfully.");
        }



        [HttpGet("order/getall")]
        public async Task<IActionResult> GetOrder([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {




            var result = await _adminService.GetOrderPagingAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("order/getcount")]
        public async Task<IActionResult> GetCountOrder()
        {




            var result = await _adminService.GetTotalOrderCountAsync();
            return Ok(result);
        }
        [HttpPost("order/changestatus/")]
        public async Task<IActionResult> ChangeStatusOrder([FromBody] string orderid, OrderStatus newStatus)
        {
             await _adminService.UpdateOrderStatusAsync(orderid, newStatus);
            return Ok("Order Change Role successfully");
        }


        [HttpGet("book/getcountbooks")]
        public async Task<IActionResult> GetCountBook()
        {
            var result = await _adminService.GetTotalBookCountAsync();
            return Ok(result);
        }

        [HttpPost("book/createbook")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
           await _adminService.CreateBookAsync(createBookDTO);
            return Ok("Create book successfully");
        }
        [HttpPost("book/updatebook")]
        public async Task<IActionResult> UpdateBook([FromQuery] string bookid, [FromBody] UpdateBookDTO updateBookDTO)
        {
            await _adminService.UpdateBookAsync(bookid, updateBookDTO);
            return Ok("Update book successfully");
        }
        [HttpDelete("book/deletebook")]
        public async Task<IActionResult> DeleteBook([FromBody] string bookid)
        {
            if (string.IsNullOrWhiteSpace(bookid))
            {
                return BadRequest("Book ID cannot be empty.");
            }
            await _adminService.DeleteBookAsync(bookid);
            return Ok("Book deleted successfully.");
        }




        [HttpGet("getallcategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("getcountcategory")]
        public async Task<IActionResult> GetCountCategory()
        {
            var result = await _categoryService.GetCategoryCount();
            return Ok(result);
        }
        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CreateCategoryDTO createCategoryDTO)
        {
         
            await _categoryService.AddCategoryAsync(createCategoryDTO.CategoryName, createCategoryDTO.Description, createCategoryDTO.Image);
            return Ok("Category added successfully.");
        }

        [HttpDelete("deletecategory")]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] int categoryId)
        {
            if (categoryId < 0) // Fix: Proper validation for categoryId
            {
                return BadRequest("Category id must be a positive integer.");
            }
            await _categoryService.DeleteCategoryAsync(categoryId); 
            return Ok("Category deleted successfully.");
        }
    }
}
