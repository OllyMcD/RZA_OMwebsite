using RZA_OMwebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace RZA_OMwebsite.Services
{
    public class RoomService
    {
        private readonly TlS2303831RzaContext _context;
        public RoomService(TlS2303831RzaContext context)
        {
            _context = context;
        }
        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }
        public async Task<Room> GetRoomAsync(int roomNumber)
        {
            return await _context.Rooms.FirstAsync(r => r.RoomNumber == roomNumber);
        }
    }
}

