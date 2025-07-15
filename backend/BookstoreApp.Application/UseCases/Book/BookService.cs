using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Book
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisService _redisService;

        public BookService(IUnitOfWork unitOfWork, IRedisService redisService)
        {
            _unitOfWork = unitOfWork;
            _redisService = redisService;

        }

        public async Task<List<BookDTO>> Get8ProductNew()
        {
            const string cacheKey = "new_books:8";
            var cachedJson = await _redisService.GetAsync(cacheKey);
            if (cachedJson != null)
            {
                var cachedBooks = JsonSerializer.Deserialize<List<BookDTO>>(cachedJson);
                if (cachedBooks != null)
                    return cachedBooks;
            }

            var books = await _unitOfWork.Book.Get8ProductNew();

            // Ensure null safety by using null-coalescing operator
            var bookDtos = books?.Select(b => new BookDTO
            {
                Id = b.Id ?? string.Empty, // Handle possible null value
                Title = b.Title,
                Image = b.Image,
                Authors = b.Authors,
                Description = b.Description,
                Rating = b.Rating,
                Status = b.Status,
                RatingCount = b.RatingCount,
                ReviewCount = b.ReviewCount,
                NumPages = b.NumPages,
                Price = b.Price,
                Genres = b.Genres ?? new List<string>(), // Handle possible null value
                CreatedAt = b.CreatedAt
            }).ToList() ?? new List<BookDTO>(); // Handle possible null value for books

            return bookDtos;
        }

        public async Task<PaginationDTO<BookDTO>> GetBooksPagingAsync(int page, int pageSize, List<string>? genres)
        {
            var (books, total) = await _unitOfWork.Book.GetBooksPagingAsync(page, pageSize, genres);

            // Ensure null safety by using null-coalescing operator
            var bookDtos = books?.Select(b => new BookDTO
            {
                Id = b.Id ?? string.Empty, // Handle possible null value
                Title = b.Title,
                Image = b.Image,
                Authors = b.Authors,
                Description = b.Description,
                Rating = b.Rating,
                Status = b.Status,
                RatingCount = b.RatingCount,
                ReviewCount = b.ReviewCount,
                NumPages = b.NumPages,
                Price = b.Price,
                Genres = b.Genres ?? new List<string>(), // Handle possible null value
                CreatedAt = b.CreatedAt
            }).ToList() ?? new List<BookDTO>(); // Handle possible null value for books

            return new PaginationDTO<BookDTO>
            {
                Items = bookDtos,
                TotalItems = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<BookDTO> GetProductAsync(string id)
        {


            var books = await _unitOfWork.Book.GetByIdAsync(id);
            if (books == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            return new BookDTO
            {
                Id = books.Id ?? string.Empty, // Handle possible null value
                Title = books.Title,
                Image = books.Image,
                Authors = books.Authors,
                Description = books.Description,
                Rating = books.Rating,
                Status = books.Status,
                RatingCount = books.RatingCount,
                ReviewCount = books.ReviewCount,
                NumPages = books.NumPages,
                Price = books.Price,
                Genres = books.Genres ?? new List<string>(), // Handle possible null value
                CreatedAt = books.CreatedAt
            };
        }

        public async Task<List<BookHeroDTO>> GetProductForHero()
        {
            const string cacheKey = "Hero_books:7";
            var cachedJson = await _redisService.GetAsync(cacheKey);
            if (cachedJson != null)
            {
                var cachedBooks = JsonSerializer.Deserialize<List<BookHeroDTO>>(cachedJson);
                if (cachedBooks != null)
                    return cachedBooks;
            }
            var books = await _unitOfWork.Book.GetProductForHeroAsync();

            // Ensure null safety by using null-coalescing operator
            var bookDtos = books?.Select(b => new BookHeroDTO
            {
                Id = b.Id ?? string.Empty, // Handle possible null value
                Title = b.Title,
                Image = b.Image,
                Authors = b.Authors,
                Description = b.Description,
                Rating = b.Rating,
                Status = b.Status,
                Price = b.Price,
                CreatedAt = b.CreatedAt
            }).ToList() ?? new List<BookHeroDTO>(); // Handle possible null value for books
            var jsonToCache = JsonSerializer.Serialize(bookDtos);
            await _redisService.SetAsync(cacheKey, jsonToCache, TimeSpan.FromMinutes(10));

            return bookDtos;
        
        }
    }
}
