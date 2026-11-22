using System.ComponentModel.DataAnnotations;

namespace JwtLearningSystem.Shared.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        [StringLength(70)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
