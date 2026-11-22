using JwtLearningSystem.Shared.Constants;
using JwtLearningSystem.Shared.Data;
using JwtLearningSystem.Shared.DTOs.Auth;
using JwtLearningSystem.Shared.Entities;
using JwtLearningSystem.Shared.Exceptions;
using JwtLearningSystem.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtLearningSystem.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserResponseDto?> LoginAsync(LoginRequestDto loginRequest, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(loginRequest);

            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return null;
            }

            string normalizedEmail = loginRequest.Email.Trim().ToUpperInvariant();

            User? user = await _context.Users.AsNoTracking().Include(user => user.Roles).FirstOrDefaultAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);

            if (user is null || !user.IsActive)
            {
                return null;
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt, user.PasswordHashIterations);

            if (!isPasswordValid)
            {
                return null;
            }

            return MapToUserResponse(user);
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto registerRequest, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(registerRequest);

            ArgumentException.ThrowIfNullOrWhiteSpace(registerRequest.Email);
            ArgumentException.ThrowIfNullOrWhiteSpace(registerRequest.Password);

            string normalizedEmail = registerRequest.Email.Trim().ToUpperInvariant();

            bool emailExists = await _context.Users.AnyAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);

            if (emailExists)
            {
                throw new EmailAlreadyExistsException();
            }

            Role? customerRole = await _context.Roles.FirstOrDefaultAsync(role => role.NormalizedName == AppRoles.Customer.ToUpperInvariant(), cancellationToken);
            
            if (customerRole is null)
            {
                throw new InvalidOperationException("Customer role was not found.");
            }

            var passwordResult = _passwordHasher.HashPassword(registerRequest.Password);

            var user = new User
            {
                // PropertyName = Value


                Id = Guid.NewGuid(),
                Email = registerRequest.Email.Trim(),
                NormalizedEmail = normalizedEmail,
                FirstName = registerRequest.FirstName.Trim(),
                LastName = registerRequest.LastName.Trim(),
                PasswordHash = passwordResult.Hash,
                PasswordSalt = passwordResult.Salt,
                PasswordHashIterations = passwordResult.Iterations,
                Roles = new List<Role> { customerRole }
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return MapToUserResponse(user);
        }


        private static UserResponseDto MapToUserResponse(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = user.Roles.Select(role => role.Name).ToList()
            };
        }
    }
}
