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
        public static string HashPassword(string password)
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

        // Point-based password strength evaluation
        public static string CheckPasswordStrength(string password)
        {
            int score = 0;

            // Length points
            if (password.Length >= 8) score++;
            if (password.Length >= 12) score++;

            // Contains uppercase and lowercase letters
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsLower)) score++;

            // Contains numbers
            if (password.Any(digits.Contains)) score++;

            // Contains special characters
            if (password.Any(specialCharacters.Contains)) score++;

            // Assign strength rating based on score
            return score switch
            {
                1 => "Very Weak",
                2 => "Weak",
                3 => "Medium",
                4 => "Strong",
                >= 5 => "Very Strong",
                <= 0 => "Unknown"
            };
        }

        // Checks if a password is strong or very strong
        public static bool IsStrongPassword(string password)
        {
            return CheckPasswordStrength(password) is "Strong" or "Very Strong";
        }
    }

    public class UserSession
    {
        public int UserId { get; set; }
    }
}
