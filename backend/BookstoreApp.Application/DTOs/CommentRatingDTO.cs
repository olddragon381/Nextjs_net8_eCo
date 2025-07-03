using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class CommentRatingDTO
    {
        public string? FullName { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }

        public string? Comment { get; set; }
        public ScoreEnum RatingValue { get; set; } = ScoreEnum.One;
        
        
    }
}
