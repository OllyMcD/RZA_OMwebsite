using Microsoft.EntityFrameworkCore;
using RZA_OMwebsite.Models;

namespace RZA_OMwebsite.Services
{
    public class TicketService
    {
        private readonly TlS2303831RzaContext _context;
        public TicketService(TlS2303831RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        public async Task AddTicketAsync(Ticket newTicket)
        {
            await _context.Tickets.AddAsync(newTicket);
            await _context.SaveChangesAsync();
        }
    }
}