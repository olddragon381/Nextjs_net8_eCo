using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookstoreApp.Infrastructure.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IMongoCollection<Rating> _ratingCollection;

        public RatingRepository(IMongoDatabase database)
        {
            _ratingCollection = database.GetCollection<Rating>("Rating");
        }


        

        public async Task AddRatingAsync(string userId, RatingDTO rating)
        {
            if (rating == null)
            {
                throw new ArgumentNullException(nameof(rating), "Rating cannot be null");
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }


            await _ratingCollection.InsertOneAsync(new Rating
            {
                UserId = userId,
                BookId = rating.BookId,
                Comment = rating.Comment,
                RatingValue = rating.RatingValue,
                CreatedAt = DateTime.UtcNow

            });
        }

        public Task DeleteRatingAsync(string userId, string bookId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetRatingCountAsync(string bookId)
        {
           return _ratingCollection.CountDocumentsAsync(r => r.BookId == bookId).ContinueWith(task => (int)task.Result);
        }

        public async Task<List<CommentRatingDTO>> GetCommentRatingsByBookIdAsync(string bookId)
        {
            var filter = Builders<Rating>.Filter.Eq(r => r.BookId, bookId);

            var ratings = await _ratingCollection.Find(filter).ToListAsync();

            // Nếu bạn dùng AutoMapper
            return ratings.Select(r => new CommentRatingDTO
            {

                FullName = "",
                Comment = r.Comment,
                RatingValue = r.RatingValue,
               
                UserId = r.UserId,
            }).ToList();

        }

        public Task UpdateRatingAsync(string userId, string bookId, int newRating)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserHasRatedBookAsync(string userId, string bookId)
        {
            throw new NotImplementedException();
        }
    }
}
