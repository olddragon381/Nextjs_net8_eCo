using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IOTPService
    {
        Task SendOtpAsync(string userId, string email);
        Task<bool> VerifyOtpAsync(string userId, string otp,string email);

        Task<bool> ResetPasswordAsync(string userId, string email);
    }
}
