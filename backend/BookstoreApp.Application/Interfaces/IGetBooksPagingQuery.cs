using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IGetBooksPagingQuery
    {
        /// <summary>
        /// Retrieves a paginated list of books.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that represents the asynchronous operation, containing a paginated list of books.</returns>
        Task GetBooksPagingAsync(int pageNumber, int pageSize);
    }
}
