using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class UpdateBookDTO
    {
        public required string Title { get; set; }
        public string? Image { get; set; } // URL or path to the book cover image
        public required string Authors { get; set; } // Comma-separated list of authors
        public string? Description { get; set; } // Book description

       
        public List<string> Genres { get; set; } = new(); // List of genres (e.g., Fiction, Non-Fiction, Mystery, etc.)
        public int? NumPages { get; set; } = 0; // Number of pages in the book
        public decimal Price { get; set; } = 0.0m; // Price of the book, using decimal for currency representation
        public required ProductStatus Status { get; set; }
    }
}
