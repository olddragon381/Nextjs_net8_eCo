using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<GetNameCategoryDTO>> GetAllCategoriesAsync();
        public Task<List<GetNameCategoryDTO>> Get7CategoriesAsync();
        public Task AddCategoryAsync(string categoryName, string? Description, string? Image);
        public Task UpdateCategoryAsync(int categoryId, string newCategoryName, string? newDescription);
        public Task DeleteCategoryAsync(int categoryId);
      
    }
}
