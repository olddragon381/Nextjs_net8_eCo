using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
      
        private readonly IUserProfileService _userProfileService;
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
        }

        [Authorize]
        [HttpGet("getuserprofile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();


            var user = await _userProfileService.GetUserProfileAsync(userId);
            if (user == null)
                return NotFound();
            var result = new
            {
                Email = user.Email,
                FullName = user.FullName,
                Profile = new
                {
                   
                    user.Profile.NameForOrder,
                    user.Profile.Phone,
                    user.Profile.Address
                }
            };
            return Ok(result);


        }

        [Authorize]
        [HttpPost("updateuserprofile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDTO requset)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var user = await  _userProfileService.UpdateUserProfileAsync(userId, requset);
            if(user == null) // Added a proper null check
                   return NotFound();
            
           
            return Ok(user);


        }


        [Authorize]
        [HttpPost("updateuserfullname")]
        public async Task<IActionResult> UpdateUserFullname([FromBody] UpdateFullnameRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (string.IsNullOrEmpty(request.FullName))
                return BadRequest("FullName cannot be null or empty.");

            await _userProfileService.UpdateUserFullNameAsync(userId, request.FullName);
           

            return Ok("Ok thay doi thanh cong");


        }
    }
}
