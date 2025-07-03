using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Auth;
using BookstoreApp.Application.UseCases.Book;
using BookstoreApp.Application.UseCases.Cart;
using BookstoreApp.Application.UseCases.Category;
using BookstoreApp.Application.UseCases.Comment;
using BookstoreApp.Application.UseCases.Order;
using BookstoreApp.Application.UseCases.User;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.DI
{
    public static class DependencyIApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProtectAuth, ProtectAuth>();
    

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRecommendationSystem, RecommendationService>();
            services.AddScoped<IRatingService,RatingService>();
            return services;
        }
    }
}
