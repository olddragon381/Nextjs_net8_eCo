using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Cart;
using BookstoreApp.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Tests.UseCase
{
    public class CartServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IBookRepository> _bookRepoMock;
        private readonly Mock<ICartRepository> _cartRepoMock;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepoMock = new Mock<IUserRepository>();
            _bookRepoMock = new Mock<IBookRepository>();
            _cartRepoMock = new Mock<ICartRepository>();

            _unitOfWorkMock.SetupGet(u => u.Users).Returns(_userRepoMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Book).Returns(_bookRepoMock.Object);
            _unitOfWorkMock.SetupGet(u => u.Cart).Returns(_cartRepoMock.Object);

            _cartService = new CartService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddToCartAsync_Should_Add_Item_To_New_Cart()
        {
            // Arrange  
            string userId = "user123";
            string bookId = "book123";
            var request = new AddToCartRequestDTO { BookId = bookId, Quantity = 2 };

            _userRepoMock.Setup(u => u.GetByIdOneUserAsync(userId))
                .ReturnsAsync(new UserInfoDTO { FullName = "ten", Email = "241@fsd" });

            _bookRepoMock.Setup(b => b.GetByIdAsync(bookId))
                .ReturnsAsync(new Book { Id = bookId, Title = "fsfs", Price = 100m });

            _cartRepoMock.Setup(c => c.GetByUserIdAsync(userId))
                .ReturnsAsync((Domain.Entities.Cart?)null); // Fix: Explicitly cast null to nullable type  

            _cartRepoMock.Setup(c => c.SaveAsync(It.IsAny<Domain.Entities.Cart>()))
                .Returns(Task.CompletedTask);

            // Act  
            await _cartService.AddToCartAsync(userId, request);

            // Assert  
            _cartRepoMock.Verify(c => c.SaveAsync(It.Is<Domain.Entities.Cart>(cart =>
                cart.UserId == userId &&
                cart.Items.Count == 1 &&
                cart.Items[0].BookId == bookId &&
                cart.Items[0].Quantity == 2 &&
                cart.Items[0].Price == 100m
            )), Times.Once);




        }

        [Fact]
        public async Task AddToCartAsync_Throws_When_User_Not_Found()
        {
            // Arrange
            string userId = "user1";
            var request = new AddToCartRequestDTO { BookId = "book1", Quantity = 1 };

            _userRepoMock.Setup(x => x.GetByIdOneUserAsync(userId))
                         .ReturnsAsync((UserInfoDTO)null);

            // Act
            Func<Task> act = () => _cartService.AddToCartAsync(userId, request);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                     .WithMessage("User not found");
        }

        [Fact]
        public async Task AddToCartAsync_Throws_When_Book_Not_Found()
        {
            // Arrange
            string userId = "user1";
            var request = new AddToCartRequestDTO { BookId = "book1", Quantity = 1 };

            _userRepoMock.Setup(x => x.GetByIdOneUserAsync(userId))
                         .ReturnsAsync(new UserInfoDTO { FullName="f", Email="22@" });

            _bookRepoMock.Setup(x => x.GetByIdAsync(request.BookId))
                         .ReturnsAsync((Book)null);

            // Act
            Func<Task> act = () => _cartService.AddToCartAsync(userId, request);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                     .WithMessage("Book with ID 'book1' not found.");
        }

        [Fact]
        public async Task AddToCartAsync_Throws_When_Quantity_Is_Zero()
        {
            // Arrange
            string userId = "user1";
            var request = new AddToCartRequestDTO { BookId = "book1", Quantity = 0 };

            _userRepoMock.Setup(x => x.GetByIdOneUserAsync(userId))
                         .ReturnsAsync(new UserInfoDTO { FullName = "f", Email = "22@" });

            _bookRepoMock.Setup(x => x.GetByIdAsync(request.BookId))
                         .ReturnsAsync(new Book { Id = "book1",Title ="f",Price = 100 });

            // Act
            Func<Task> act = () => _cartService.AddToCartAsync(userId, request);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                     .WithMessage("Quantity must be greater than zero");
        }

        [Fact]
        public async Task AddToCartAsync_Increments_Quantity_If_Item_Exists()
        {
            // Arrange
            string userId = "user1";
            string bookId = "book1";
            var request = new AddToCartRequestDTO { BookId = bookId, Quantity = 2 };

            var existingCart = new Domain.Entities.Cart
            {
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem { BookId = bookId, Quantity = 3, Price = 100 }
                }
            };

            _userRepoMock.Setup(x => x.GetByIdOneUserAsync(userId))
                         .ReturnsAsync(new UserInfoDTO { FullName = "f", Email = "22@" });

            _bookRepoMock.Setup(x => x.GetByIdAsync(bookId))
                         .ReturnsAsync(new Book { Id = bookId, Title = "f", Price = 100 });

            _cartRepoMock.Setup(x => x.GetByUserIdAsync(userId))
                         .ReturnsAsync(existingCart);

            _cartRepoMock.Setup(x => x.SaveAsync(It.IsAny<Domain.Entities.Cart>()))
                         .Returns(Task.CompletedTask);

            // Act
            await _cartService.AddToCartAsync(userId, request);

            // Assert
            existingCart.Items.Should().ContainSingle(i => i.BookId == bookId && i.Quantity == 5);
            _cartRepoMock.Verify(x => x.SaveAsync(It.IsAny<Domain.Entities.Cart>()), Times.Once);
        }
    


}
}
