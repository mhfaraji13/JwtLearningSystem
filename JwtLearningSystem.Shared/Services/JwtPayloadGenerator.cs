using JwtLearningSystem.Shared.DTOs.Auth;
using JwtLearningSystem.Shared.DTOs.Jwt;

namespace JwtLearningSystem.Shared.Services
{
    public class JwtPayloadGenerator
    {
        private readonly Base64UrlEncoder _base64UrlEncoder;

        public JwtPayloadGenerator(Base64UrlEncoder base64UrlEncoder)
        {
            _base64UrlEncoder = base64UrlEncoder;
        }
        public JwtPayloadDto Generate(UserResponseDto user, string issuer, string audience)
        
        {
            ArgumentNullException.ThrowIfNull(user);

            ArgumentException.ThrowIfNullOrWhiteSpace(issuer);
            ArgumentException.ThrowIfNullOrWhiteSpace(audience);

            DateTimeOffset now = DateTimeOffset.UtcNow;

            return new JwtPayloadDto
            {
                Subject = user.Id.ToString(),
                Email = user.Email,
                Roels = user.Roles,

                IssuedAt = now.ToUnixTimeSeconds(),
                ExpiresAt = now.AddHours(1).ToUnixTimeSeconds(),

                Issuer = issuer,
                Audience = audience,

                JwtId = Guid.NewGuid().ToString()
            };
        }

        public string GenerateBase64Url(JwtPayloadDto payload)
        {
            ArgumentNullException.ThrowIfNull(payload);
            string json = payload.GenerateJson();
            return _base64UrlEncoder.Encode(json);
        }
    }
}
