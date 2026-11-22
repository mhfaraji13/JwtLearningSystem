namespace JwtLearningSystem.Shared.DTOs.Auth
{
    public class AuthResponseDto
    {
        public required string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public DateTimeOffset ExpiresAt { get; set; }
        public required UserResponseDto User { get; set; }
    }
}
