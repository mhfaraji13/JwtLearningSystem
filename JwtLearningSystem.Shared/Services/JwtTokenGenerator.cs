
using JwtLearningSystem.Shared.DTOs.Auth;
using JwtLearningSystem.Shared.DTOs.Jwt;

namespace JwtLearningSystem.Shared.Services
{
    public class JwtTokenGenerator
    {
        private readonly Base64UrlEncoder _base64UrlEncoder;
        private readonly JwtPayloadGenerator _payloadGenerator;
        private readonly JwtSigningInputGenerator _signingInputGenerator;
        private readonly JwtSignatureGenerator _signatureGenerator;

        public JwtTokenGenerator(
            Base64UrlEncoder base64UrlEncoder,
            JwtPayloadGenerator payloadGenerator,
            JwtSigningInputGenerator signingInputGenerator,
            JwtSignatureGenerator signatureGenerator)
        {
            _base64UrlEncoder = base64UrlEncoder;
            _payloadGenerator = payloadGenerator;
            _signingInputGenerator = signingInputGenerator;
            _signatureGenerator = signatureGenerator;
        }

        public string Generate(
            UserResponseDto user,
            string issuer,
            string audience,
            string secretKey)
        {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentException.ThrowIfNullOrWhiteSpace(issuer);
            ArgumentException.ThrowIfNullOrWhiteSpace(audience);
            ArgumentException.ThrowIfNullOrWhiteSpace(secretKey);

            // 1. Generate Header
            JwtHeaderDto header = new JwtHeaderDto();

            string headerJson = header.GenerateJson();

            string encodedHeader = _base64UrlEncoder.Encode(headerJson);

            // 2. Generate Payload
            JwtPayloadDto payload = _payloadGenerator.Generate(
                user,
                issuer,
                audience);

            string encodedPayload = _payloadGenerator.GenerateBase64Url(payload);

            // 3. Generate Signing Input
            string signingInput = _signingInputGenerator.Generate(
                encodedHeader,
                encodedPayload);

            // 4. Generate Signature
            string signature = _signatureGenerator.Generate(
                signingInput,
                secretKey);

            // 5. Generate Final JWT
            return $"{signingInput}.{signature}";
        }
    }
}
