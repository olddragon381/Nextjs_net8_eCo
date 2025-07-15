using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Application.UseCases.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BookstoreApp.Application.UseCases.Auth
{
    public class OTPService : IOTPService
    {
        private readonly IEmailService _emailService;
        private readonly IRedisService _redis;
        private static readonly Random _random = new Random();
         
        public OTPService(IEmailService emailService, IRedisService redis)
        {
            _emailService = emailService;
            _redis = redis;
        }
        public async Task SendOtpAsync(string userId, string email)
        {
            var otp = GenerateOtp();
            var key = $"otp:reset:{userId}";
            await _redis.SetAsync(key, otp, TimeSpan.FromMinutes(10));


            string body = $"Mã đặt lại mật khẩu là: <b>{otp}</b>";
            await _emailService.SendAsync(email, "Quên mật khẩu", body);
        }

        public async Task<bool> VerifyOtpAsync(string userId, string otp, string email)
        {
            var key = $"otp:reset:{userId}";
            var stored = await _redis.GetAsync(key);
            if (stored != otp) return false;

            await _redis.RemoveAsync(key);
            var key2 = $"otp:resetpassword:{userId}";
            await _redis.SetAsync(key2, email, TimeSpan.FromMinutes(10));

            return true;
        }
        public async Task<bool> ResetPasswordAsync(string userId, string email)
        {
            var key2 = $"otp:resetpassword:{userId}";
            var stored = await _redis.GetAsync(key2);
            if (stored != email) return false;

            return true;
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }

        
    }
}
