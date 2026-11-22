using JwtLearningSystem.Shared.DTOs.Auth;
using JwtLearningSystem.Shared.Exceptions;
using JwtLearningSystem.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtLearningSystem.HS256.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserResponseDto>> Register(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                UserResponseDto user = await _authService.RegisterAsync(request, cancellationToken);

                return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }

           
        }


        [HttpPost("login")]

      //  public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request, CancellationToken cancellationToken)
                    public async Task<ActionResult<UserResponseDto>> Login(LoginRequestDto request, CancellationToken cancellationToken)

        {
            UserResponseDto? user = await _authService.LoginAsync(request, cancellationToken);

            if (user is null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(user);
        }

    }
}
