using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Domain.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)] 
        public int Id { get; set; } = new Random().Next(1, int.MaxValue);
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
