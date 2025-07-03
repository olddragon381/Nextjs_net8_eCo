using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CommentController : ControllerBase
    {
        private readonly IRatingService _ratingServices;


        public CommentController(IRatingService ratingServices)
        {
            _ratingServices = ratingServices ?? throw new ArgumentNullException(nameof(ratingServices));
        }

        [Authorize]
        [HttpPost("addrating")]
        public async Task<IActionResult> AddComment(RatingDTO rating)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _ratingServices.AddRatingAsync(userId,rating);
            return(Ok("Comment added successfully!"));
        }



        [HttpGet("getcomment/{bookid}")]
        public async Task<IActionResult> GetComment(string bookid)
        {
            
            var comments = await _ratingServices.GetCommentByBookIdAsync(bookid);
            return Ok(comments);
        }

        [HttpGet("getcommentcount/{bookid}")]
        public async Task<IActionResult> GetCommentCount(string bookid)
        {
           

            var comments = await _ratingServices.GetCommentCountAsync(bookid);
            return Ok(comments);
        }



    }
}
