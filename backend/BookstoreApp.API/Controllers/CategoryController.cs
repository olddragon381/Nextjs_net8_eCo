using BookstoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }
        [HttpGet("get7category")]
        public async Task<IActionResult> Get7Category()
        {
            var result = await _categoryService.Get7CategoriesAsync();
            return Ok(result);
        }

        [HttpGet("getallcategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }
        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] string categoryName, string? description, string? image)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Category name cannot be empty.");
            }
            await _categoryService.AddCategoryAsync(categoryName, description, image);
            return Ok("Category added successfully.");
        }
    }
}
