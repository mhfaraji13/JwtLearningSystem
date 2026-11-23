using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLearningSystem.Shared.Services
{
    public class JwtSigningInputGenerator
    {
        public string Generate(string encodedHeader, string encodedPayload)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(encodedHeader);
            ArgumentException.ThrowIfNullOrWhiteSpace(encodedPayload);

            return $"{encodedHeader}.{encodedPayload}";
        }
    }
}
