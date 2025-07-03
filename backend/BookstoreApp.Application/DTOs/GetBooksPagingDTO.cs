using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DTOs
{
    public class GetBooksPagingQuery : IRequest<PaginationDTO<BookDTO>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Genre { get; set; }

        public GetBooksPagingQuery(int page, int pageSize, List<string> genre)
        {
            Page = page;
            PageSize = pageSize;
            Genre = genre;
        }
    }

}
