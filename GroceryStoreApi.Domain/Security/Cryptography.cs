using System.Security.Cryptography;

namespace GroceryStoreApi.Domain.Security
{
    public static class Cryptography
    {
        public static string GenerateHash(this string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return System.Text.Encoding.UTF8.GetString(hashedBytes);
        }
    }
}