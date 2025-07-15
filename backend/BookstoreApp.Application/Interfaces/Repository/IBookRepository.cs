using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> Get8ProductNew();
        Task<Book?> GetByIdAsync(string id);

        Task<List<Book>> SameBookByGenreAsync(string bookid, int topN);
        Task<List<Book>> GetByCategoryIdAsync(int categoryId);
        Task<List<Book>> SearchAsync(string searchTerm);
        Task CreateAsync(CreateBookDTO book);
        Task UpdateAsync(string bookid ,UpdateBookDTO book);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(int id);
        Task<List<Book>> GetProductForHeroAsync();
        Task<(List<Book>, int total)> GetBooksPagingAsync(int page, int pageSize,List<string>? genres);

        Task<bool> CheckBookInDatabaseAsync(string bookid);
        Task<int> GetTotalBooksCountAsync();
    }   
}
