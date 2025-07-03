using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using BookstoreApp.Domain.Enums;

namespace BookstoreApp.Domain.Entities
{
   

    public class Book
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }


        [BsonElement("title")]
        public required string Title { get; set; }
        [BsonElement("image")]
        public string? Image { get; set; } // URL or path to the book cover image
        [BsonElement("authors")]
        public required string Authors { get; set; } // Comma-separated list of authors
        [BsonElement("description")]
        public string? Description { get; set; } // Book description
        [BsonElement("rating")]
        public double Rating { get; set; } = 0.0; // Average rating of the book
        [BsonElement("ratingCount")]
        public int RatingCount { get; set; } = 0; // Number of ratings received
        [BsonElement("reviewCount")]
        public int ReviewCount { get; set; } = 0; // Number of reviews received
        [BsonElement("genres")]
        public List<string> Genres { get; set; } = new(); // List of genres (e.g., Fiction, Non-Fiction, Mystery, etc.)
       
        [BsonElement("product_id")]
        public required Int32 ProductId { get; set; } // Unique identifier for the product, could be an ISBN or custom ID
        [BsonElement("num_pages")]
        public int? NumPages { get; set; } = 0; // Number of pages in the book
        [BsonElement("price")]
        public decimal Price { get; set; } = 0.0m; // Price of the book, using decimal for currency representation
        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public required ProductStatus Status { get; set; }
        

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
