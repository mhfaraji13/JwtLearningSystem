using JwtLearningSystem.Shared.DTOs.Jwt;
using JwtLearningSystem.Shared.Services;

namespace JwtLearningSystem.Tests;


public class Base64UrlEncoderTests
{
    [Fact]

    public void Encoder_ShouldGenerateBase64Url()
    {
        // Arrange 

        var header = new JwtHeaderDto
        {
            Algorithm = "HS256",
            Type = "JWT"

        };
        var jsonHeader = header.GenerateJson();
        Base64UrlEncoder base64UrlEncoder = new Base64UrlEncoder();
        // Act 

        var result = base64UrlEncoder.Encode(jsonHeader);

        // Assert 

        Assert.Equal(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
            result
            );
    }
}
