using BookstoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class RatingDTO
    {

        public required string BookId { get; set; }
        public ScoreEnum RatingValue { get; set; } = ScoreEnum.One;
        public string? Comment { get; set; }
    }
}
