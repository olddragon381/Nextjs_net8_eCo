using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Domain.Enums;

namespace BookstoreApp.Domain.Entities
{
    public class UserInteraction
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; } 
        //public string UserId { get; set; }
        //public string BookId { get; set; }
        //public ActionTypeUserInteraction ActionType { get; set; } = ActionTypeUserInteraction.View;
        //public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
