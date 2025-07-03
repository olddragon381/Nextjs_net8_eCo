using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IRecommendationSystem
    {
        public Task<List<BookDTO>> GetRecommendationsAsync(string bookId, int topN = 5);
    }
    public class RecommendationResult
    {
        public string? BookId { get; set; }
        public double Score { get; set; }
    }

    public class RecommendationResponse
    {
        public List<RecommendationResult> Recommendations { get; set; } = new List<RecommendationResult>();
    }
}
