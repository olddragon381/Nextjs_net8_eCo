using BookstoreApp.Application.UseCases.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Tests.UseCase
{
    public class ProtectAuthTests
    {
        private readonly ProtectAuth _protectAuth;

        public ProtectAuthTests()
        {
            _protectAuth = new ProtectAuth();
        }

        [Fact]
        public void GenerateSalt_ReturnsUniqueNonEmptyBase64String()
        {
            // Act
            var salt1 = _protectAuth.generateSalth();
            var salt2 = _protectAuth.generateSalth();

            // Assert
            Assert.False(string.IsNullOrEmpty(salt1));
            Assert.False(string.IsNullOrEmpty(salt2));
            Assert.NotEqual(salt1, salt2); // Mỗi lần phải khác nhau
        }

        [Fact]
        public void HashPassword_WithSameInput_ReturnsSameHash()
        {
            // Arrange
            var password = "MySecret123";
            var salt = _protectAuth.generateSalth();

            // Act
            var hash1 = _protectAuth.hashPassword(password, salt);
            var hash2 = _protectAuth.hashPassword(password, salt);

            // Assert
            Assert.Equal(hash1, hash2); // Băm giống nhau nếu đầu vào giống nhau
        }

        [Fact]
        public void HashPassword_WithDifferentSalt_ReturnsDifferentHash()
        {
            // Arrange
            var password = "MySecret123";
            var salt1 = _protectAuth.generateSalth();
            var salt2 = _protectAuth.generateSalth();

            // Act
            var hash1 = _protectAuth.hashPassword(password, salt1);
            var hash2 = _protectAuth.hashPassword(password, salt2);

            // Assert
            Assert.NotEqual(hash1, hash2); // Salt khác nhau thì hash phải khác
        }
    }
}
