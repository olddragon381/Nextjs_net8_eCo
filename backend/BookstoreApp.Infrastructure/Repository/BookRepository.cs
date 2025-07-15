using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _bookCollection;
        private readonly IRedisService _redisService;

        public BookRepository(IMongoDatabase database, IRedisService redisService)
        {
            _bookCollection = database.GetCollection<Book>("Books");
            _redisService = redisService;
        }

        public async Task<bool> CheckBookInDatabaseAsync(string bookid)
        {
            return await _bookCollection.Find(b => b.Id == bookid).AnyAsync();
        }

        public async Task CreateAsync(CreateBookDTO book)
        {
            await _bookCollection.InsertOneAsync(new Book
           {
          
               Title = book.Title,
               Image = book.Image,
               Authors = book.Authors,
               Description = book.Description,
               Rating = book.Rating,
               Status = book.Status,
               RatingCount = book.RatingCount,
               ReviewCount = book.ReviewCount,
               NumPages = book.NumPages,
               Price = book.Price,
               Genres = book.Genres ?? new List<string>(),
               CreatedAt = DateTime.UtcNow
           });
        }

        public async Task DeleteAsync(string id)
        {
            await _bookCollection.DeleteOneAsync(b => b.Id == id);
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> Get8ProductNew()
        {
            var filter = Builders<Book>.Filter.In(b => b.Status, new[] {
    ProductStatus.InStock,
    ProductStatus.OnSale
});


            var books = await _bookCollection.Find(filter)
                                             .SortByDescending(b => b.CreatedAt)
                                             .Limit(8)
                                             .ToListAsync();

            return books;
        }

        public Task<List<Book>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<(List<Book>, int total)> GetBooksPagingAsync(int page, int pageSize, List<string>? genres)
        {
            FilterDefinition<Book> filter;

            if (genres == null || !genres.Any())
            {
                filter = Builders<Book>.Filter.Empty; 
            }
            else
            {
                filter = Builders<Book>.Filter.AnyIn(b => b.Genres, genres);
            }

            var total = await _bookCollection.CountDocumentsAsync(filter);

            var books = await _bookCollection.Find(filter)
                .SortByDescending(b => b.CreatedAt)
                                             .Skip((page - 1) * pageSize)
                                             .Limit(pageSize)
                                             .ToListAsync();
            return (books, (int)total);
        }

        public Task<List<Book>> GetByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Book?> GetByIdAsync(string id)
        {
            return await _bookCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetProductForHeroAsync()
        {
            var filter = Builders<Book>.Filter.And(
    Builders<Book>.Filter.Eq(b => b.Status, ProductStatus.OnSale),
    Builders<Book>.Filter.Ne(b => b.Image, null),
    Builders<Book>.Filter.Ne(b => b.Authors, null),
    Builders<Book>.Filter.Ne(b => b.Title, null)
);

            var books = await _bookCollection
    .Find(filter)
    .SortByDescending(b => b.CreatedAt)
    .Limit(7)
    .ToListAsync();

            return books;
        }

        public async Task<int> GetTotalBooksCountAsync()
        {
          
            return (int)await _bookCollection.CountDocumentsAsync(FilterDefinition<Book>.Empty);
        }

        public async Task<List<Book>> SameBookByGenreAsync(string bookid, int topN)
        {
            var sourceBook = await _bookCollection.Find(b => b.Id == bookid).FirstOrDefaultAsync();
            if (sourceBook == null || sourceBook.Genres == null || !sourceBook.Genres.Any())
            {
                return new List<Book>();
            }

            var filter = Builders<Book>.Filter.And(
                Builders<Book>.Filter.Ne(b => b.Id, bookid),
                Builders<Book>.Filter.AnyIn(b => b.Genres, sourceBook.Genres)
            );

            var relatedBooks = await _bookCollection.Find(filter)
                                                    .Limit(topN)
                                                    .ToListAsync();

            return relatedBooks;
        }

        public Task<List<Book>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string bookid, UpdateBookDTO book)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, bookid);
            var updateDefinition = Builders<Book>.Update
                .Set(b => b.Title, book.Title)
                .Set(b => b.Image, book.Image)
                .Set(b => b.Authors, book.Authors)
                .Set(b => b.Description, book.Description)
                .Set(b => b.Genres, book.Genres)
                .Set(b => b.NumPages, book.NumPages)
                .Set(b => b.Price, book.Price)
                .Set(b => b.Status, book.Status);

            var updatedBook = await _bookCollection.FindOneAndUpdateAsync(
                filter,
                updateDefinition,
                new FindOneAndUpdateOptions<Book> { ReturnDocument = ReturnDocument.After }
            );

            if (updatedBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookid} not found.");
            }
        }
    }
}
