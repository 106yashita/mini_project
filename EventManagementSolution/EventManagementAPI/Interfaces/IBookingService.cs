using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IBookingService
    {
        public Task<Booking> MakeBooking(BookingDTO bookingDTO);

    }
}
