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
            try
            {
                // Check if the username exists
                if (await UsernameExistsAsync(customer.Username))
                {
                    return false; // Username already exists
                }


                // Hash the password before saving
                //customer.Password = PasswordUtils.HashPassword(customer.Password);
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                return true; // Customer successfully added
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
           
        }

        // Validate login credentials
        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
                if (customer != null)
                {
                   
                    if (customer.Password == password)
                    {
                        return true; // Login successful
                    }
                }
                return false; // Login failed
            }
            catch (Exception)
            {
                Console.WriteLine("error");
                return false;
            }

        }
        #region hidden
        // Get customer by username
        public async Task<Customer?> GetCustomerByUsernameAsync(string username)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
        }


        #endregion


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

        // Change password for a customer
          public async Task ChangePassword(int customerId, string hashedOldPassword, string hashedNewPassword)
        {
            Customer? customer = await _context.Customers.SingleOrDefaultAsync(
                c => c.CustomerId == customerId && 
                c.Password == hashedOldPassword);
            if (customer != null)
            {
                customer.Password = hashedNewPassword;
                await _context.SaveChangesAsync();
            }
        }

    }
}
