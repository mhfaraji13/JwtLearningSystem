using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLearningSystem.Shared.Services
{
    public class Base64UrlEncoder
    {
        public string Encode(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            string base64 = Convert.ToBase64String(bytes);

            string base64url = base64
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');

            return base64url;
        }

        public string Encode(byte[] bytes)
        {
            string base64 = Convert.ToBase64String(bytes);
            return base64
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');
        }
    }
}
