using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class ChangePasswordDTO
    {
      
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public ChangePasswordDTO(string currentPassword, string newPassword, string confirmNewPassword)
        {
          
         
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmNewPassword = confirmNewPassword;
        }
      
    }
}
