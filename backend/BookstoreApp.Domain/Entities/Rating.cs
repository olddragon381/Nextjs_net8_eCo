using BookstoreApp.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class Rating
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        public required string UserId { get; set; }
        public required string BookId { get; set; }
        public string? Comment { get; set; }
        public ScoreEnum RatingValue { get; set; } = ScoreEnum.One;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
