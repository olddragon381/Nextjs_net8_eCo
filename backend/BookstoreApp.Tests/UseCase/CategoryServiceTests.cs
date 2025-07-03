using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Category;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Tests.UseCase
{
    public class CategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();

            
            _mockUnitOfWork.Setup(u => u.Category).Returns(_mockCategoryRepository.Object);

            _categoryService = new CategoryService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task AddCategoryAsync_CallsRepositoryOnce()
        {
            // Arrange
            var name = "Science";
            var description = "Science related books";

            // Act
            await _categoryService.AddCategoryAsync(name, description);

            // Assert
            _mockCategoryRepository.Verify(r => r.AddCategoryAsync(name, description), Times.Once);
        }
        [Fact]
        public async Task AddCategoryAsync_CallsRepositoryOnce_NoDescription()
        {
            // Arrange
            var name = "Science";
            var description = "";

            // Act
            await _categoryService.AddCategoryAsync(name, description);

            // Assert
            _mockCategoryRepository.Verify(r => r.AddCategoryAsync(name, description), Times.Once);
        }


        [Fact]
        public async Task DeleteCategoryAsync_CallsRepositoryOnce()
        {
            var categoryId = "abc123";

            await _categoryService.DeleteCategoryAsync(categoryId);

            _mockCategoryRepository.Verify(r => r.DeleteCategoryAsync(categoryId), Times.Once);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsCategoryNames()
        {
            // Arrange
            var categories = new List<string> { "Tech", "History" };
            _mockCategoryRepository.Setup(r => r.GetAllCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(categories, result);
        }

        [Fact]
        public async Task UpdateCategoryAsync_CallsRepositoryWithCorrectParameters()
        {
            var id = "abc123";
            var newName = "Updated";
            var newDesc = "Updated Desc";

            await _categoryService.UpdateCategoryAsync(id, newName, newDesc);

            _mockCategoryRepository.Verify(r => r.UpdateCategoryAsync(id, newName, newDesc), Times.Once);
        }
    }
}
