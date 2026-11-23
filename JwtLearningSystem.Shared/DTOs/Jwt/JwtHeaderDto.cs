using System.Text.Json;
using System.Text.Json.Serialization;

namespace JwtLearningSystem.Shared.DTOs.Jwt
{
    public class JwtHeaderDto
    {
        [JsonPropertyName("alg")]
        public string Algorithm { get; set; } = "HS256";
        [JsonPropertyName("typ")]
        public string Type { get; set; } = "JWT";

        public string GenerateJson()
        {
            string json = JsonSerializer.Serialize(this);
            return json;
        }
        
    }
}
