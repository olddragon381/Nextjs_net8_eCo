using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class BookDTO
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; } // URL or path to the book cover image
        public required string Authors { get; set; } // Comma-separated list of authors
        public string? Description { get; set; } // Book description
        public double Rating { get; set; } = 0.0; // Average rating of the book
        public int RatingCount { get; set; } = 0; // Number of ratings received
        public int ReviewCount { get; set; } = 0; // Number of reviews received
        public List<string> Genres { get; set; } = new(); // List of genres (e.g., Fiction, Non-Fiction, Mystery, etc.)
        public int? NumPages { get; set; }
        public required ProductStatus Status { get; set; } // Status of the product (e.g., InStock, OutOfStock, etc.)
        public decimal Price { get; set; } = 0.0m; // Price of the book, using decimal for currency representation
        public DateTime CreatedAt { get; set; }

       

        
    }
}
