using System;
using System.Security.Cryptography;
using System.Text;

namespace Pizzastycznie.Authentication
{
    public static class PasswordHashHelper
    {
        public static string GenerateHash(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

            return BitConverter.ToString(hashedBytes).Replace("-", "", StringComparison.InvariantCulture).ToLower();
        }

        public static string GenerateSalt()
        {
            var bytes = new byte[128 / 8];
            using var keyGenerator = RandomNumberGenerator.Create();
            keyGenerator.GetBytes(bytes);

            return BitConverter.ToString(bytes).Replace("-", "", StringComparison.InvariantCulture).ToLower();
        }
    }
}