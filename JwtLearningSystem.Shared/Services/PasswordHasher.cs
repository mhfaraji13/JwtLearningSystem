

using JwtLearningSystem.Shared.Interfaces;
using System.Security.Cryptography;

namespace JwtLearningSystem.Shared.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 600_000;


        public (string Hash, string Salt, int Iterations) HashPassword(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
            return (
             Convert.ToBase64String(hash),
             Convert.ToBase64String(salt),
             Iterations
                   );
        }


        // This method has the duty to check Is password when user add in Login matches with the password save at Database.
        public bool VerifyPassword(string password, string storedHash, string storedSalt, int storedIterations)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            if (string.IsNullOrWhiteSpace(storedHash) || string.IsNullOrWhiteSpace(storedSalt) || storedIterations <= 0)
            {
                return false;
            }

            byte[] salt;
            byte[] expectedHash;

            try
            {
                salt = Convert.FromBase64String(storedSalt);
                expectedHash = Convert.FromBase64String(storedHash);
            }
            catch (FormatException)
            {
                return false;
            }

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, storedIterations, HashAlgorithmName.SHA256, expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(
               actualHash,
               expectedHash
               );







        }
    }
}
