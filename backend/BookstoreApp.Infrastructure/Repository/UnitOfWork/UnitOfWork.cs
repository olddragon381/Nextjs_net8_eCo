using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public ICategoryRepository Category { get; }
        public IOrderRepository Order { get; }
        public ICartRepository Cart { get; }
        public IBookRepository Book { get; }
        public IDiscountRepository Discount { get; } // Assuming you have a discount repository as well

        public IRatingRepository Rating { get; }

        public UnitOfWork(IUserRepository users, IRatingRepository rating, ICategoryRepository category, ICartRepository cart, IBookRepository book, IDiscountRepository discount, IOrderRepository order)
        {
            Users = users;
            Category = category;
            Cart = cart;
            Book = book;
            Discount = discount;
            Order = order;
            Rating = rating;
        }




        public Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }
        public Task BeginTransactionAsync()
        {
            // Implement transaction logic here
            return Task.CompletedTask;
        }
        public Task CancelAsync()
        {
            // Implement cancel logic here
            return Task.CompletedTask;
        }
    }
}
