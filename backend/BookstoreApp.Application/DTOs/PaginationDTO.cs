﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class PaginationDTO<T>
    {
        public required IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; } 
        public int PageSize { get; set; }   
        public int PageNumber { get; set; }
    }

}
