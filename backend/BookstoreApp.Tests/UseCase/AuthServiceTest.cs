using BookstoreApp.Application.Interfaces.Repository;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.UseCases.Auth;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Application.DTOs;
using BookstoreApp.Domain.Entities;
using BookstoreApp.Domain.Enums;

namespace BookstoreApp.Tests.UseCase
{
    public class AuthServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IJwtProvider> _mockJwtProvider;
        private readonly AuthService _authService;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IProtectAuth> _mockProtectAuth;   


        public AuthServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockJwtProvider = new Mock<IJwtProvider>();
            _mockUserRepo = new Mock<IUserRepository>();
            _mockProtectAuth = new Mock<IProtectAuth>();
            _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
            _authService = new AuthService(_mockUnitOfWork.Object, _mockJwtProvider.Object, _mockProtectAuth.Object);
        }

        [Fact]
        public async Task LoginAsync_ReturnsToken_WhenCredentialsAreCorrect()
        {
            // Arrange  
            var loginDTO = new LoginDTO { Email = "test@example.com", PasswordHash = "rawpass" };

            var user = new User
            {
                Id = "123",
                FullName = "Test User", 
                Email = "test@example.com",
                PasswordHash = "hashed",
                Salt = "salt"
            };

            _mockUserRepo.Setup(r => r.GetByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
            _mockProtectAuth.Setup(p => p.hashPassword(loginDTO.PasswordHash, user.Salt)).Returns("hashed");
            _mockJwtProvider.Setup(p => p.GenerateToken(It.IsAny<User>())).Returns("fake-jwt");

            // Act  
            var result = await _authService.LoginAsync(loginDTO);

            // Assert  
            Assert.Equal("fake-jwt", result.Token);
            Assert.Equal("123", result.UserId);
            Assert.Equal("Test User", result.Fullname);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task LoginAsync_Throws_WhenUserNotFound()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "notfound@example.com", PasswordHash = "any" };
            _mockUserRepo.Setup(r => r.GetByEmailAsync(loginDTO.Email)).ReturnsAsync((User?)null);

            // Act & Assert
            _mockUserRepo.Setup(r => r.GetByEmailAsync(loginDTO.Email)).ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _authService.LoginAsync(loginDTO));
        }


        [Fact]
        public async Task LoginAsync_Throws_WhenNullValidate()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "", PasswordHash = "" };
           

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _authService.LoginAsync(loginDTO));
            Assert.Equal("Thông tin đăng nhập không hợp lệ", ex.Message);
        }
        [Fact]
        public async Task LoginAsync_Throws_WhenPasswordIncorrect()
        {
            // Arrange  
            var loginDTO = new LoginDTO { Email = "test@example.com", PasswordHash = "wrongpass" };

            var user = new User
            {
                Id = "1",
                FullName = "Test User",
                Email = "test@example.com",
                Salt = "salt123",
                PasswordHash = new ProtectAuth().hashPassword("correctpass", "salt123")
            };

            _mockUserRepo.Setup(r => r.GetByEmailAsync(loginDTO.Email)).ReturnsAsync(user);
            _mockProtectAuth.Setup(p => p.hashPassword(loginDTO.PasswordHash, user.Salt))
                            .Returns(new ProtectAuth().hashPassword("wrongpass", "salt123"));

            // Act & Assert  
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _authService.LoginAsync(loginDTO));
            Assert.Equal("Tài khoản hoặc mật khẩu không đúng", ex.Message);

        }
            [Fact]
        public async Task LoginAsync_ShouldThrowValidationException_WhenPasswordMismatch()
        {
            var loginDto = new LoginDTO { Email = "test@example.com", PasswordHash = "wrong" };

            var user = new User
            {
                Id = "1",
                Email = loginDto.Email,
                FullName = "Test User",
                PasswordHash = "correcthash",
                Salt = "salt"
            };

            _mockUserRepo.Setup(r => r.GetByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _mockProtectAuth.Setup(p => p.hashPassword(loginDto.PasswordHash, user.Salt)).Returns("wronghash");

            await Assert.ThrowsAsync<ValidationException>(() => _authService.LoginAsync(loginDto));
        }





        [Fact]
        public async Task RegisterAsync_Allows_SuperAdmin_ToCreateAdmin()
        {
            // Arrange
            var dto = new RegisterDTO
            {
                Email = "newadmin@example.com",
                FullName = "New Admin",
                Salt = "salt123",
                PasswordHash = "123456",
                Role = Role.Admin
            };

            _mockUserRepo.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);
            _mockJwtProvider.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("fake-token");
            _mockProtectAuth.Setup(p => p.generateSalth()).Returns("salt123");
            _mockProtectAuth.Setup(p => p.hashPassword(dto.PasswordHash, "salt123")).Returns("123456");
            // Act
            var result = await _authService.RegisterAsync(dto, currentUserRole: "SuperAdmin");

            // Assert
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal("fake-token", result.Token);
        }

        [Fact]
      
        public async Task RegisterAsync_Throws_WhenEmailExists()
        {
            // Arrange  
            var registerDTO = new RegisterDTO
            {
                Email = "test@example.com",
                FullName = "Test User",
                PasswordHash = "123456",
                Salt = "salt",

            };

            var existingUser = new User
            {
                Email = "test@example.com",
                FullName = "Existing User",
                PasswordHash = "hashedpass",
                Salt = "salt"
            };

            _mockUserRepo.Setup(r => r.GetByEmailAsync(registerDTO.Email))
                         .ReturnsAsync(existingUser);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() =>
                _authService.RegisterAsync(registerDTO, currentUserRole: "Admin"));

            Assert.Equal("Email đã tồn tại", ex.Message);
        }
        

        [Fact]
        public async Task RegisterAsync_Throws_WhenMissingFields()
        {
            // Arrange
            var invalidDTO = new RegisterDTO
            {
                Email = "",
                FullName = "",
                PasswordHash = string.Empty,
                Salt = string.Empty

            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _authService.RegisterAsync(invalidDTO));
            Assert.Equal("Thông tin đăng ký không hợp lệ", ex.Message);
        }

        [Fact]
        public async Task RegisterAsync_User_CanCreate_User()
        {
            var dto = new RegisterDTO
            {
                Email = "newuser@example.com",
                FullName = "New User",
                PasswordHash = "123456",
                Salt = "salt",
                Role = Role.User
            };

            _mockUserRepo.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);
            _mockJwtProvider.Setup(p => p.GenerateToken(It.IsAny<User>())).Returns("fake-token");
            _mockProtectAuth.Setup(p => p.generateSalth()).Returns("salt");
            _mockProtectAuth.Setup(p => p.hashPassword(dto.PasswordHash, "salt")).Returns("123456");
            var result = await _authService.RegisterAsync(dto, currentUserRole: "User");

            Assert.Equal(dto.Email, result.Email);
            Assert.Equal("fake-token", result.Token);
            _mockUserRepo.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]

        public async Task RegisterAsync_Throws_WhenRoleNotAllowed()
        {
            // Arrange
            var dto = new RegisterDTO
            {
                Email = "test2@example.com",
                FullName = "Somebody",
                PasswordHash = "123456",
                Salt = "salt",
                Role = Domain.Enums.Role.Admin // Cần quyền cao
            };

            _mockUserRepo.Setup(x => x.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _authService.RegisterAsync(dto, currentUserRole: "User"));

            Assert.Equal("Bạn không có quyền tạo người dùng với vai trò này.", ex.Message);
        }
    }
}
