using Microsoft.EntityFrameworkCore;
using RZA_OMwebsite.Models;

namespace RZA_OMwebsite.Services
{
    public class AttractionService
    {
        private readonly TlS2303831RzaContext _context;

        public AttractionService(TlS2303831RzaContext context)
        {
            _context = context;
        }

        public async Task<List<Attraction>> GetAttractionsAsync()
        {
            return await _context.Attractions.ToListAsync();
        }
    }
}
