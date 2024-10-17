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
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
           
    }
}
