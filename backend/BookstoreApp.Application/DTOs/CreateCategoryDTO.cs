using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class CreateCategoryDTO
    {
        
        
            public string CategoryName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Image { get; set; }
        
    }
}
