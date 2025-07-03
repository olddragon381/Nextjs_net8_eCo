using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        public Task<List<GetNameCategoryDTO>> GetAllCategoriesAsync();
        public Task<GetNameCategoryDTO?> GetCategoryByIdAsync(int categoryId);
        public Task AddCategoryAsync(string categoryName, string? Description, string? Image);
        public Task UpdateCategoryAsync(int categoryId, string newCategoryName, string? newDescription);
        public Task DeleteCategoryAsync(int categoryId);
    }
}
