using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface IRatingRepository
    {
        Task AddRatingAsync(string userId, RatingDTO rating);
        Task<int> GetRatingCountAsync(string bookId);
        Task<List<CommentRatingDTO>> GetCommentRatingsByBookIdAsync(string bookId);
        Task<bool> UserHasRatedBookAsync(string userId, string bookId);
        Task UpdateRatingAsync(string userId, string bookId, int newRating);
        Task DeleteRatingAsync(string userId, string bookId);
    }
}
