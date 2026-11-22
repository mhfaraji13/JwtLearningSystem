using System.ComponentModel.DataAnnotations;
namespace JwtLearningSystem.Shared.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        // This attribute specified max length for this property
        [StringLength(70)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
    }
}
