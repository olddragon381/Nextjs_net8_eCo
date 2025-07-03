using BookstoreApp.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICategoryRepository Category { get; }
        ICartRepository Cart { get; }
        IBookRepository Book { get; }
        IDiscountRepository Discount { get; }

        IRatingRepository Rating { get; }
        IOrderRepository Order { get; }
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelAsync();
    }
}
