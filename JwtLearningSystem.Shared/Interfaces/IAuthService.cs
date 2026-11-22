using JwtLearningSystem.Shared.DTOs.Auth;

namespace JwtLearningSystem.Shared.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(RegisterRequestDto registerRequest, CancellationToken cancellationToken = default);
        Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequest, CancellationToken cancellationToken = default);
    }
}
