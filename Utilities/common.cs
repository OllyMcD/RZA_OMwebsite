using System.Text;
using System.Security.Cryptography;

namespace RZA_OMwebsite.Utilities
{
    public static class PasswordUtils
    {
        private static readonly char[] specialCharacters = new char[]
        {
            '!', '£', '$', '%', '^', '&', '*', '(', ')', '-', '=', '_', '+', '[', ']', '{', '}', ';', ':', '@', '#', '~', '<', '>'
        };

        private static readonly char[] digits = new char[]
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };

        // Hashing function for password storage
        public static string HashPassword(string? password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                var sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // "x2" for hexadecimal formatting
                }
                return sb.ToString();
            }
        }
    }
    public class UserSession
    {
        public int customerId { get; set; }
    }
}
