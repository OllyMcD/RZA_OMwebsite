using Microsoft.EntityFrameworkCore;
using RZA_OMwebsite.Models;

namespace RZA_OMwebsite.Services
{
    public class TrackingService
    {
        private readonly TlS2303831RzaContext _context;

        public TrackingService(TlS2303831RzaContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(string action, HttpContext context)
        {
            // Get the user's IP address
            
            string? ipAddress = context.Connection.RemoteIpAddress?.ToString();
            string username = context.User?.Identity?.Name ?? "Anonymous";
            await Task.Delay(950);

            // Create the tracking entry
            var tracking = new Tracking
            {
                IpAddress = ipAddress,
                Action = action,
                Timestamp = DateTime.Now
            };

            // Save the entry to the database
            _context.Trackings.Add(tracking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tracking>> GetTrackingDataAsync()
        {
            return await _context.Trackings
                .OrderByDescending(t => t.Timestamp) // Sort by timestamp, most recent first
                .ToListAsync();
        }
    }

}
