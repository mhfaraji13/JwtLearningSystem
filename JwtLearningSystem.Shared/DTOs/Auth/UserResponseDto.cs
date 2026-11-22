namespace JwtLearningSystem.Shared.DTOs.Auth
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
