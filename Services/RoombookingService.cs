using RZA_OMwebsite.Models;
using Microsoft.EntityFrameworkCore;
namespace RZA_OMwebsite.Services
{
    public class RoombookingService
    {
        private readonly TlS2303831RzaContext _context;
        public RoombookingService(TlS2303831RzaContext context)
        {
            _context = context;
        }
        public async Task AddRoombookingAsync(Customer customer, Room room, DateOnly startDate, int duration)
        {
            Roombooking? newRoombooking = new Roombooking();
            newRoombooking.Customer = customer;
            newRoombooking.Room = room;
            newRoombooking.StartDate = startDate;
            newRoombooking.EndDate = startDate.AddDays(duration);
            var temps = await _context.Roombookings.ToListAsync();

            var temp = await _context.Roombookings.Where(r => r.CustomerId == customer.CustomerId &&
                                                         r.RoomNumber == room.RoomNumber &&
                                                         r.StartDate == newRoombooking.StartDate).FirstOrDefaultAsync();
            if (temp == null)
            {
                await _context.Roombookings.AddAsync(newRoombooking);
                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("could not book room");
            }
            newRoombooking = null;
        }
        public async Task<List<Roombooking>> GetRoombookingsFromCustomer(Customer customer)
        {
            return await _context.Roombookings.Where(rb => rb.CustomerId == customer.CustomerId).ToListAsync();
        }
        public async Task<List<Roombooking>> GetRoombookingsFromCustomer(int id)
        {
            return await _context.Roombookings.Where(rb => rb.CustomerId == id).ToListAsync();
        }
        public async Task DeleteBooking(Roombooking roombooking)
        {
            _context.Roombookings.Remove(roombooking);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserRoomBookings(int userid)
        {
            _context.Roombookings.RemoveRange(_context.Roombookings.Where(t => t.CustomerId == userid));
            await _context.SaveChangesAsync();
        }
    }
}