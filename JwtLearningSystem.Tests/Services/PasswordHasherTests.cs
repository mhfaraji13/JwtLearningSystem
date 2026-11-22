using JwtLearningSystem.Shared.Services;
namespace JwtLearningSystem.Tests.Services
{
    public class PasswordHasherTests
    {
        [Fact]
        public void HashPassword_ShouldGenerateHashAndSalt()
        {
            // Arrange
            string password = "Admin123+";
            PasswordHasher passwordHasher = new PasswordHasher();
            // Act 
            var result = passwordHasher.HashPassword(password);
            // Display
            Console.WriteLine($"Hash: {result.Hash}");
            Console.WriteLine($"Salt: {result.Salt}");
            Console.WriteLine($"Iterations: {result.Iterations}");
            // Assert
            Assert.NotNull(result.Hash);
            Assert.NotNull(result.Salt);
            Assert.NotEqual(password, result.Hash);
            Assert.Equal(32, Convert.FromBase64String(result.Hash).Length);
            Assert.Equal(16, Convert.FromBase64String(result.Salt).Length);
            Assert.Equal(600_000, result.Iterations);
        }
        [Fact]
        public void HashPassword_ShouldGenerateDifferentHashesForSamePassword()
        {
            //Arrange
            string password = "Admin123*";
            PasswordHasher passwordHasher = new PasswordHasher();
            // Act 
            var firstResult = passwordHasher.HashPassword(password);
            var secondResult = passwordHasher.HashPassword(password);
            // Assert 
            Assert.NotEqual(firstResult.Salt, secondResult.Salt);
            Assert.NotEqual(firstResult.Hash, secondResult.Hash);
        }
        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            //Arrange 
            string password = "Admin123*";
            PasswordHasher passwordHasher = new PasswordHasher();
            var hashResult = passwordHasher.HashPassword(password);
            //Act 
            bool result = passwordHasher.VerifyPassword(password, hashResult.Hash, hashResult.Salt, hashResult.Iterations);
            //Assert 
            Assert.True(result);
        }
        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            //Arrange 
            string correctPassword = "Admin123*";
            string incorrectPassword = "WrongPassword123*";
            PasswordHasher passwordHasher = new PasswordHasher();
            var hashResult = passwordHasher.HashPassword(correctPassword);
            // Act 
            bool result = passwordHasher.VerifyPassword(incorrectPassword, hashResult.Hash, hashResult.Salt, hashResult.Iterations);
            // Assert
            Assert.False(result);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void HashPassword_ShouldThrowException_WhenPasswordIsEmptyOrWhitespace(string password)
        {
            // Arrange 
            PasswordHasher passwordHasher = new PasswordHasher();
            // Act & Assert 
            Assert.Throws<ArgumentException>(() => passwordHasher.HashPassword(password));
        }
        [Theory]
        [InlineData("invalid-hash", "valid-salt")]
        [InlineData("valid-hash", "invalid-salt")]
        [InlineData("", "")]
        public void VerifyPassword_ShouldReturnFalse_WhenStoredDataIsInvalid(string storedHash, string storedSalt)
        {
            // Arrange
            PasswordHasher passwordHasher = new PasswordHasher();
            string password = "Admin123*";
            // Act
            bool result = passwordHasher.VerifyPassword(password, storedHash, storedSalt, 600_000);
            // Assert 
            Assert.False(result);
        }
    }
}
