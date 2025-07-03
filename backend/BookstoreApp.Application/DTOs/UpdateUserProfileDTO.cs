using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class UpdateUserProfileDTO
    {
        
        public string NameForOrder { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Add a public parameterless constructor to resolve CS0122  
        public UpdateUserProfileDTO()
        {
           
            NameForOrder = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
        }
    }
}
