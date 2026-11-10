using System.ComponentModel.DataAnnotations;

namespace JwtLearningSystem.Shared.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [StringLength(70, MinimumLength = 10, ErrorMessage = "Email must be between 10 and 70 characters.")]
        [EmailAddress]
        public required string Email { get; set; }
        public required string PasswordSalt { get; set; }
        public required string PasswordHash { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsActive { get; set; } = true;
        [StringLength(25, MinimumLength = 2, ErrorMessage = "FirstName must be between 2 and 25 characters.")]
        public required string FirstName { get; set; }
        [StringLength(25, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 25 characters.")]
        public required string LastName { get; set; }
        public List<Role> Roles { get; set; } = new();

    }
}
