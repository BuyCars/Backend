using System.Security.Cryptography;
using System.Text;

namespace eBuyCars.BusinessLogic.Structure
{
    public static class TokenService
    {
        private const string SaltData = "eBuyCars_SecretSalt_2024";
        public static string GenerateToken()
        {
            var tokenBytes = new byte[32];
            RandomNumberGenerator.Fill(tokenBytes);
            return Convert.ToBase64String(tokenBytes)
                .Replace("/", "")
                .Replace("+", "")
                .Replace("=", "")
                .ToLower();
        }

        public static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            var originalBytes = Encoding.Default.GetBytes(password + SaltData);
            var encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}
