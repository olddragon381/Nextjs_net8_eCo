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

        public BookRepository(IMongoDatabase database)
        {
            _bookCollection = database.GetCollection<Book>("Books");
        }

        public async Task<bool> CheckBookInDatabaseAsync(string bookid)
        {
            return await _bookCollection.Find(b => b.Id == bookid).AnyAsync();
        }

        public Task CreateAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
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
                .Aggregate()
                .Match(filter)
                .Sample(7) // Lấy 7 sách ngẫu nhiên
                .ToListAsync();

            return books;
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
    }
}
