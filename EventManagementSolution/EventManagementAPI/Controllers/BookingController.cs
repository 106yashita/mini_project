using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        [Route("booking")]
        public async Task<IActionResult> MakeBooking(BookingDTO bookingDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Booking booking = await _bookingService.MakeBooking(bookingDTO);
                    return StatusCode(StatusCodes.Status201Created, new
                    {
                        Message = "booking done successfully",
                        booking
                    });
                }
                catch (Exception ex)
                {
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            else
            {
                return BadRequest("All details are not provided. Please check the object");
            }
        }
    }
}
