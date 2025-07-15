using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProtectAuth _IProtectAuth;
        private readonly IOTPService _otpService;
        public AuthService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IProtectAuth protectAuth, ILogger<AuthService> logger, IOTPService otpService)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _IProtectAuth = protectAuth;
            _logger = logger;
            _otpService = otpService;
        }

        public async Task ChangePassword(string userid, ChangePasswordDTO changePassword)
        {
            if (changePassword == null)
                throw new ArgumentNullException(nameof(changePassword), "Change password data cannot be null");

            if (string.IsNullOrWhiteSpace(changePassword.CurrentPassword) || string.IsNullOrWhiteSpace(changePassword.NewPassword))
                throw new ValidationException("Old password and new password cannot be empty");
            if(changePassword.NewPassword == changePassword.CurrentPassword)
                throw new ValidationException("New password cannot be the same as the old password");
            if (changePassword.NewPassword != changePassword.ConfirmNewPassword )
                throw new ValidationException("New password is the same as the ConfirmNewPassword");

            var salt = _IProtectAuth.generateSalth();
            var hashPassword = _IProtectAuth.hashPassword(changePassword.NewPassword, salt);


            await _unitOfWork.Users.ChangePasswordAsync(userid, hashPassword, salt);
            
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(email);
            if (user == null)
               throw new ValidationException("Email không tồn tại");    
            await _otpService.SendOtpAsync(user.Id, user.Email);

        }

        public async Task<AuthResponeDTO> LoginAsync(LoginDTO loginDTO)
        {

            // 1. Validate đầu vào
            LoginValidator.Validate(loginDTO);

            // 2. Kiểm tra người dùng tồn tại
            var user = await _unitOfWork.Users.GetByEmailAsync(loginDTO.Email);
            if (user == null)
                throw new ValidationException("Tài khoản hoặc mật khẩu không đúng");

            // 3. Hash lại mật khẩu từ input và so sánh với DB
            var hashedInput = _IProtectAuth.hashPassword(loginDTO.PasswordHash, user.Salt);
            if (hashedInput != user.PasswordHash)
                throw new ValidationException("Tài khoản hoặc mật khẩu không đúng");

            // 4. Sinh token và trả về
            var token = _jwtProvider.GenerateToken(user);

            return new AuthResponeDTO
            {
                UserId = user.Id,
                Fullname = user.FullName,
                Email = user.Email,
                UserRole = user.UserRole.ToString(),
                Expiration = DateTime.UtcNow.AddHours(1),
                Token = token
            };
        }

        public async Task<AuthResponeDTO> RegisterAsync(RegisterDTO registerDTO, string? currentUserRole = null)
        {
            RegisterValidator.Validate(registerDTO);

            var checkEmail = await _unitOfWork.Users.GetByEmailAsync(registerDTO.Email);
            if (checkEmail != null)
                throw new ValidationException("Email đã tồn tại");

            var newRole = registerDTO.Role != default ? registerDTO.Role : Domain.Enums.Role.User;
            var currentRole = Enum.TryParse(currentUserRole, out Role parsedRole) ? parsedRole : Role.User;

            if (!currentRole.CanCreateRole(newRole))
                throw new UnauthorizedAccessException("Bạn không có quyền tạo người admin hay staff với vai trò này.");
               

            var salt = _IProtectAuth.generateSalth();
            var hashPassword = _IProtectAuth.hashPassword(registerDTO.Password, salt);

            var user = new Domain.Entities.User
            {
                FullName = registerDTO.Fullname,
                Email = registerDTO.Email,
                PasswordHash = hashPassword,
                Salt = salt,
               
                UserRole = newRole
            };

            await _unitOfWork.Users.CreateAsync(user);

            var token = _jwtProvider.GenerateToken(user);
            Console.WriteLine($"Generated token: {token}");
            return new AuthResponeDTO
            {
                UserId = user.Id,
                Fullname = user.FullName,
                Email = user.Email,
                UserRole = user.UserRole.ToString(),    
                Expiration = DateTime.UtcNow.AddHours(1),
                Token = token
            };
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO dto)
        {

            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Không tìm thấy người dùng");
            _ = _otpService.ResetPasswordAsync(user.Id, dto.Email)
           .ContinueWith(task =>
           {
               if (task.IsFaulted)
               {
                   _logger.LogError(task.Exception, "Error Reset Password");
                   throw new ValidationException("Reset mat khau sai");
               }
               return false;
           });




            if (string.IsNullOrWhiteSpace(dto.NewPassword))
                throw new ValidationException("Mật khẩu mới không được để trống");
            var salt = _IProtectAuth.generateSalth();
            var hashPassword = _IProtectAuth.hashPassword(dto.NewPassword, salt);


            await _unitOfWork.Users.ChangePasswordAsync(user.Id, hashPassword, salt);
        }

        public async Task<bool> VeryfyOTPAsync(VerifyotpDTO dto)
        {
            var userid = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (userid == null)
                throw new ValidationException("Email không tồn tại");
            if (string.IsNullOrWhiteSpace(dto.OTP))
                throw new ValidationException("OTP không được để trống");

            _ = _otpService.VerifyOtpAsync(userid.Id, dto.OTP, dto.Email)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        _logger.LogError(task.Exception, "Error verifying OTP");
                        throw new ValidationException("OTP không hợp lệ hoặc đã hết hạn");
                    }
                    return false;
                });

     
            return true;
        }
    }
}
