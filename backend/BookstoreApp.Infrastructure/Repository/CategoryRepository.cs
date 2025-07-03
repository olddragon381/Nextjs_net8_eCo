using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(IMongoDatabase database)
        {
            // Ensure the database parameter is not null.
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            _categoryCollection = database.GetCollection<Category>("Category");
        }

        public async Task AddCategoryAsync(string categoryName, string? Description, string? Image)
        {
            await _categoryCollection.InsertOneAsync(new Category
            {
                Name = categoryName,
                Description = Description,
                Image = Image,
                CreatedAt = DateTime.UtcNow
            });
        }

        public Task DeleteCategoryAsync(int categoryId)
        {   
            var getCategoryTask = GetCategoryByIdAsync(categoryId);
            if (getCategoryTask == null)
            {
                throw new ArgumentException($"Category with ID {categoryId} does not exist.");
            }
            return _categoryCollection.DeleteOneAsync(c => c.Id == categoryId);

        }

        public async Task<List<GetNameCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryCollection.Find(_ => true).ToListAsync();
            return categories.Select(c => new GetNameCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Decription = c.Description,
                Image = c.Image

            }).ToList();
        }



        public async Task <GetNameCategoryDTO?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryCollection.Find(c => c.Id == categoryId).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            return new GetNameCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,

                Decription = category.Description,
                Image = category.Image
            };
        }
        

        public async Task UpdateCategoryAsync(int categoryId, string newCategoryName, string? newDescription)
        {
            var category = await _categoryCollection.Find(c => c.Id == categoryId).FirstOrDefaultAsync();

            if (category != null)
            {
                category.Name = newCategoryName;
                category.Description = newDescription;

                await _categoryCollection.ReplaceOneAsync(c => c.Id == categoryId, category);
            }
        }
    }
}
