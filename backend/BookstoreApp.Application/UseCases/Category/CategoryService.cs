using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Category
{
    public class CategoryService : ICategoryService
    {

       
        private readonly IUnitOfWork _unitOfWork;
        
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            
        }

        public async Task AddCategoryAsync(string categoryName, string? Description, string? Image)
        {
            await _unitOfWork.Category.AddCategoryAsync(categoryName, Description, Image);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _unitOfWork.Category.DeleteCategoryAsync(categoryId);
        }

        public async Task<List<GetNameCategoryDTO>> Get7CategoriesAsync()
        {
            var getNameCategory = await _unitOfWork.Category.GetAllCategoriesAsync();

            return getNameCategory.Take(7).ToList();
        }

        public async Task<List<GetNameCategoryDTO>> GetAllCategoriesAsync()

        {
            var getNameCategory = await _unitOfWork.Category.GetAllCategoriesAsync();

            return getNameCategory.ToList();
        }

        public async Task UpdateCategoryAsync(int categoryId, string newCategoryName, string? newDescription)
        {
            await _unitOfWork.Category.UpdateCategoryAsync(categoryId, newCategoryName, newDescription);
        }

        
    }
}
