using System.Security.Cryptography;
using System.Text;

namespace JwtLearningSystem.Shared.Services
{
    public class JwtSignatureGenerator
    {
        private readonly Base64UrlEncoder _base64UrlEncoder;

        public JwtSignatureGenerator(Base64UrlEncoder base64UrlEncoder)
        {
            _base64UrlEncoder = base64UrlEncoder;
        }

        public string Generate(string data, string secretKey)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(data);
            ArgumentException.ThrowIfNullOrWhiteSpace(secretKey);

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using var hmac = new HMACSHA256(keyBytes);

            byte[] signatureBytes = hmac.ComputeHash(dataBytes);

            string signature = _base64UrlEncoder.Encode(signatureBytes);

            return signature;
        }
    }
}
