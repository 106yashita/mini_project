using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly EventManagementContext _context;
        public BookingRepository(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<Booking> Add(Booking item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Booking> Delete(int key)
        {
            var booking = await Get(key);
            if (booking != null)
            {
                _context.Remove(booking);
                _context.SaveChangesAsync(true);
                return booking;
            }
            throw new NoSuchBookingException();
        }

        public async Task<Booking> Get(int key)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == key);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return bookings;
        }

        public async Task<Booking> Update(Booking item)
        {
            var booking = await Get(item.BookingId);
            if (booking != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return booking;
            }
            throw new NoSuchBookingException();
        }
    }
}
