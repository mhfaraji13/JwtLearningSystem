

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
    }
}
