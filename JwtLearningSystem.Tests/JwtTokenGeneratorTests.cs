using JwtLearningSystem.Shared.DTOs.Auth;
using JwtLearningSystem.Shared.Services;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace JwtLearningSystem.Tests
{
    public class JwtTokenGeneratorTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public JwtTokenGeneratorTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]

        public void Generate_ShouldReturnValidJwtFormats()
        {
            // Arrange

            var user = new UserResponseDto
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                Roles = new List<string> { "Customer" }
            };

            string issuer = "JwtLearningSystem";
            string audience = "JwtLearningSystem.Client";

            string secretKey = "JwtLearningSystem-Local-Test-Secret-Key-2026!";

            var base64UrlEncoder = new Base64UrlEncoder();

            var payloadGenerator = new JwtPayloadGenerator(base64UrlEncoder);

            var signingInputGenerator = new JwtSigningInputGenerator();

            var signatureGenerator = new JwtSignatureGenerator(
             base64UrlEncoder);

            var tokenGenerator = new JwtTokenGenerator(
              base64UrlEncoder,
              payloadGenerator,
              signingInputGenerator,
              signatureGenerator);

            // Act
            string token = tokenGenerator.Generate(
                user,
                issuer,
                audience,
                secretKey);


            // Assert
            Assert.False(string.IsNullOrWhiteSpace(token));

            string[] tokenParts = token.Split('.');

            Assert.Equal(3, tokenParts.Length);

            _outputHelper.WriteLine("Generated JWT:");
            _outputHelper.WriteLine(token);
        }
    }
}
