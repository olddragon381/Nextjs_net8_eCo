using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponeDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponeDTO> RegisterAsync(RegisterDTO registerDTO, string? currentUserRole = null);

        Task ChangePassword(string userid,ChangePasswordDTO changePassword);

        Task ForgotPasswordAsync(string email);
        Task<bool> VeryfyOTPAsync(VerifyotpDTO dto);
        Task ResetPasswordAsync(ResetPasswordDTO dto);
    }
}
