using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IRatingService
    {
        Task AddRatingAsync(string userid,RatingDTO ratingDTO);
        Task<int> GetCommentCountAsync(string bookid);
        Task<List<CommentRatingDTO>> GetCommentByBookIdAsync(string bookid);

    }
}
