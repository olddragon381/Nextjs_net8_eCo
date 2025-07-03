using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Application.Setting;
using BookstoreApp.Infrastructure.Cof;

using BookstoreApp.Infrastructure.Repository;
using BookstoreApp.Infrastructure.Repository.UnitOfWork;
using BookstoreApp.Infrastructure.Service;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            services.AddScoped<IJwtProvider, JwtProvider>();

            // Fix: Use the correct method to bind the configuration section
            var mongoDbSettingsSection = config.GetSection("MongoDbSettings");
            services.Configure<MongoDbSettings>(options => mongoDbSettingsSection.Bind(options));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            return services;
        }
    }
}
