using Microsoft.EntityFrameworkCore;
using RZA_OMwebsite.Models;

namespace RZA_OMwebsite.Services
{
    public class TicketbookingService
    {
        private readonly TlS2303831RzaContext _context;
        public TicketbookingService(TlS2303831RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Ticketbooking>> GetTicketbookingsAsync()
        {
            return await _context.Ticketbookings.ToListAsync();
        }
        public async Task AddTicketbookingAsync(Ticketbooking newTicketbooking)
        {
            await _context.Ticketbookings.AddAsync(newTicketbooking);
            await _context.SaveChangesAsync();
        }
    }
}