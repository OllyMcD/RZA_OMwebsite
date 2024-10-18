`using Microsoft.AspNetCore.Identity;
using RZA_OMwebsite.Models; // Ensure you have a model for your users
using System.Collections.Generic; // For user storage (could be a database in a real app)
using System.Linq; // For LINQ queries

namespace RZA_OMwebsite.Services
{
    public class AuthService
    {
        private readonly PasswordHashingService passwordHashingService;

        // Temporary storage for users (replace with database in production)
        private readonly List<User> users = new();

        private bool isLoggedIn;
        public bool IsLoggedIn => isLoggedIn;

        public AuthService(PasswordHashingService passwordHashingService)
        {
            this.passwordHashingService = passwordHashingService;
        }

        public bool Register(string username, string password, string firstName, string lastName, string email)
        {
            if (users.Any(u => u.Username == username))
            {
                return false; // User already exists
            }

            var hashedPassword = passwordHashingService.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                HashedPassword = hashedPassword,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            users.Add(newUser); // Store the user (in a real app, save to the database)
            return true; // Registration successful
        }

        public bool Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                var result = passwordHashingService.VerifyPassword(user.HashedPassword, password);
                if (result == PasswordVerificationResult.Success)
                {
                    isLoggedIn = true;
                    return true; // Login successful
                }
            }
            return false; // Login failed
        }

        public void Logout()
        {
            isLoggedIn = false;
        }
    }

    // User model for storing user information
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
