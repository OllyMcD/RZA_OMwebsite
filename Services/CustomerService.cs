using RZA_OMwebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace RZA_OMwebsite.Services
{
    public class CustomerService
    {
        private readonly TlS2303831RzaContext _context;

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

            // Add customer to the database
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return true; // Customer successfully added
        }

        // Validate login credentials
        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            // Check if the username exists
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == username && c.Password == password);

            return customer != null; // Return true if user is found
        }

    }
}
