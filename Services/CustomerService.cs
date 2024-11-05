using Microsoft.AspNetCore.Identity;
using RZA_OMwebsite.Models; // Ensure you have a model for your customers
using RZA_OMwebsite.Utilities; // For password utilities
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RZA_OMwebsite.Services
{
    public class CustomerService
    {
        private readonly TlS2303831RzaContext _context; // Your DbContext

        public CustomerService(TlS2303831RzaContext context)
        {
            _context = context;
        }

        // Method to check if username already exists
        public async Task<bool> UsernameExistsAsync(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false; // Return false if username is null or empty
            }

            return await _context.Customers.AnyAsync(c => c.Username == username);
        }

        // Add new customer if the username does not already exist
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            // Check if the username exists
            if (await UsernameExistsAsync(customer.Username))
            {
                return false; // Username already exists
            }

            // Hash the password before saving
            customer.Password = PasswordUtils.HashPassword(customer.Password);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return true; // Customer successfully added
        }

        // Validate login credentials
        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
            if (customer != null)
            {
                var hashedInputPassword = PasswordUtils.HashPassword(password);
                if (customer.Password == hashedInputPassword)
                {
                    return true; // Login successful
                }
            }
            return false; // Login failed
        }

        // Get customer by username
        public async Task<Customer?> GetCustomerByUsernameAsync(string username)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }

        // Delete customer by username
        public async Task DeleteCustomerAsync(string username)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        // Validate current password for a customer
        public async Task<bool> ValidateCurrentPasswordAsync(string username, string oldPassword)
        {
            var customer = await GetCustomerByUsernameAsync(username);
            if (customer == null) return false;

            string hashedInputPassword = PasswordUtils.HashPassword(oldPassword);
            return customer.Password == hashedInputPassword;
        }

        // Change password for a customer
        public async Task<bool> ChangePasswordAsync(string username, string newPassword)
        {
            var customer = await GetCustomerByUsernameAsync(username);
            if (customer == null) return false;

            customer.Password = PasswordUtils.HashPassword(newPassword);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
