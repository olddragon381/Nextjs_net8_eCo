using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookstoreApp.Application.UseCases.Category
{
    public class CategoryService : ICategoryService
    {

       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisService _redisService;   

        public CategoryService(IUnitOfWork unitOfWork, IRedisService redisService)
        {
            _unitOfWork = unitOfWork;
            _redisService = redisService;

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
            const string cacheKey = "getcategory:7";
            var cachedJson = await _redisService.GetAsync(cacheKey);
            if (cachedJson != null)
            {
                var cachedBooks = JsonSerializer.Deserialize<List<GetNameCategoryDTO>>(cachedJson);
                if (cachedBooks != null)
                    return cachedBooks;
            }

            var getNameCategory = await _unitOfWork.Category.GetAllCategoriesAsync();
            var jsonToCache = JsonSerializer.Serialize(getNameCategory.Take(7).ToList());
            await _redisService.SetAsync(cacheKey, jsonToCache, TimeSpan.FromMinutes(10));

 
            return getNameCategory.Take(7).ToList();
        }

        public async Task<List<GetNameCategoryDTO>> GetAllCategoriesAsync()

        {


            var getNameCategory = await _unitOfWork.Category.GetAllCategoriesAsync();

            return getNameCategory.ToList();
        }

        public async Task<int> GetCategoryCount()
        {
           return await _unitOfWork.Category.GetCountCategory();
        }

        public async Task UpdateCategoryAsync(int categoryId, string newCategoryName, string? newDescription)
        {
            await _unitOfWork.Category.UpdateCategoryAsync(categoryId, newCategoryName, newDescription);
        }

        
    }
}
