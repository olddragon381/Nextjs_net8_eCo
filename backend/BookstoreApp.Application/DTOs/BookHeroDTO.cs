using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class BookHeroDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; } // URL or path to the book cover image
        public required string Authors { get; set; } // Comma-separated list of authors
        public string? Description { get; set; } // Book description
        public double Rating { get; set; } = 0.0; // Average rating of the book

        public required ProductStatus Status { get; set; } 
        public decimal Price { get; set; } = 0.0m; 
        public DateTime CreatedAt { get; set; }

    }
}
