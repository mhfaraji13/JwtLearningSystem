namespace JwtLearningSystem.Shared.Interfaces
{
    public interface IPasswordHasher
    {
        (string Hash, string Salt, int Iterations) HashPassword(string password);

        bool VerifyPassword(string password, string storedHash, string storedSalt, int storedIterations);
    }
}
