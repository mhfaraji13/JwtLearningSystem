using JwtLearningSystem.Shared.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JwtLearningSystem.Shared.DTOs.Jwt
{
    public class JwtPayloadDto
    {
        [JsonPropertyName("sub")]
        public string Subject { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("roel")]
        public List<string> Roels { get; set; } = new();
        [JsonPropertyName("iat")]
        public long IssuedAt { get; set; }
        [JsonPropertyName("exp")]
        public long ExpiresAt { get; set; }
        [JsonPropertyName("iss")]
        public string Issuer { get; set; } = string.Empty;
        [JsonPropertyName("aud")]
        public string Audience { get; set; } = string.Empty;
        [JsonPropertyName("jti")]
        public string JwtId { get; set; } = string.Empty;

        public string GenerateJson()
        {
            string json = JsonSerializer.Serialize(this);
            return json;
        }
    }
}
