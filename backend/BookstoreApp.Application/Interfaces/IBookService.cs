using BookstoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IBookService
    {
        Task<PaginationDTO<BookDTO>> GetBooksPagingAsync(int page, int pageSize, List<string>? genres);

        Task<List<BookDTO>> Get8ProductNew();

        Task<BookDTO> GetProductAsync(string id);
        Task<List<BookHeroDTO>> GetProductForHero();
    }
}
