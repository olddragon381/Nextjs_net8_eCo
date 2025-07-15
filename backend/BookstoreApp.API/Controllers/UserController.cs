using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _unitOfWork.Users.GetByIdOneUserAsync(userId);

            if (user == null)
                return NotFound();

            // Assuming userInfo should be created from the user object
            var userInfo = new
            {
                Id = userId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.UserRole.ToString()
            };

            return Ok(userInfo);
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePassword)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var user = await _unitOfWork.Users.GetByIdOneUserAsync(userId);
                if (user == null)
                    return NotFound();

                await _authService.ChangePassword(userId, changePassword);
                return Ok(new { message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDTO dto)
        {
            await _authService.ForgotPasswordAsync(dto.Email);
            
            return Ok("OTP đã được gửi đến email");
        }
        [AllowAnonymous]
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VertifyOTP([FromBody] VerifyotpDTO dto)
        {
           var check = await _authService.VeryfyOTPAsync(dto);
            if(!check)
            {
                return BadRequest("Mã OTP không hợp lệ hoặc đã hết hạn");
            }
            return Ok("OTP đã được gửi đến email");
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            await _authService.ResetPasswordAsync(dto);
            return Ok("Mật khẩu đã được thay đổi thành công");
        }
       

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok("Logged out");
        }





     
    }
}
