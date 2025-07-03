using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Comment
{
    public class RatingService : IRatingService
    {

        private readonly IUnitOfWork _unitOfWork;
        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task AddRatingAsync(string userid,RatingDTO ratingDTO)
        {
            if (ratingDTO == null)
            {
                throw new ArgumentNullException(nameof(ratingDTO), "Rating cannot be null");
            }
            if (string.IsNullOrWhiteSpace(userid))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userid));
            }
            // Set the UserId in the ratingDTO
            var checkuser = await _unitOfWork.Users.CheckUserInDatabaseAsync(userid);
            if (!checkuser)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Add the rating to the repository
            await _unitOfWork.Rating.AddRatingAsync(userid,ratingDTO);
           

        }

        public async Task<List<CommentRatingDTO>> GetCommentByBookIdAsync(string bookid)
        {
            var checkBook = await _unitOfWork.Book.CheckBookInDatabaseAsync(bookid);
            if (!checkBook)
            {
                throw new ArgumentNullException("Book isnot finding", bookid);
            }
            var commentratings = await _unitOfWork.Rating.GetCommentRatingsByBookIdAsync(bookid);
            foreach (var comment in commentratings)
            {
                if (!string.IsNullOrWhiteSpace(comment.UserId)) 
                {
                    var user = await _unitOfWork.Users.GetUserProfileAsync(comment.UserId);
                    if (user != null)
                    {
                        comment.FullName = user.FullName;
                        comment.Email = user.Email;
                    }
                }
            }

            return commentratings;
        }

        public async Task<int> GetCommentCountAsync(string bookid)
        {
            var checkBook = await _unitOfWork.Book.CheckBookInDatabaseAsync(bookid);
            if (!checkBook)
            {
                throw new ArgumentNullException("Book isnot finding", bookid);
            }
            var ratingcount = await _unitOfWork.Rating.GetRatingCountAsync(bookid);
           

            return ratingcount;
        }
    }
}
