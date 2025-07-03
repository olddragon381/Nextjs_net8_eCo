using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Book
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public BookService(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
          
        }

        public async Task<List<BookDTO>> Get8ProductNew()
        {
            var books = await _unitOfWork.Book.Get8ProductNew();
            
           var bookDtos = books.Select(b => new BookDTO
            {
                Id = b.Id,
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
                Genres = b.Genres,
                CreatedAt = b.CreatedAt

            }).ToList();
            return bookDtos;
        }

        public async Task<PaginationDTO<BookDTO>> GetBooksPagingAsync(int page, int pageSize, List<string>? genres)
        {
            var (books, total) = await _unitOfWork.Book.GetBooksPagingAsync(page, pageSize, genres);
            var bookDtos = books.Select(b => new BookDTO
            {
                Id = b.Id,
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
                Genres = b.Genres,
                CreatedAt = b.CreatedAt

            }).ToList();

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
                Id = books.Id,
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
                Genres = books.Genres,
                CreatedAt = books.CreatedAt
            };
        }

        public async Task<List<BookHeroDTO>> GetProductForHero()
        {
            var books = await _unitOfWork.Book.GetProductForHeroAsync();

            var bookDtos = books.Select(b => new BookHeroDTO
            {
                Id = b.Id,
                Title = b.Title,
                Image = b.Image,
                Authors = b.Authors,
                Description = b.Description,
                Rating = b.Rating,
                Status = b.Status,
               
                Price = b.Price,
         
                CreatedAt = b.CreatedAt

            }).ToList();
            return bookDtos;
        }
    }
}
