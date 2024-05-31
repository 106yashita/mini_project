using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, EventResponse> _eventResponseRepository;
        private readonly IRepository<int, EventRequest> _eventRequestRepository;
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, ScheduledEvent> _scheduledEventRepository;
        public BookingService(IRepository<int, EventResponse> eventResponseRepository,
            IRepository<int, Booking> bookingRepository,
            IRepository<int, EventRequest> eventRequestRepository,
            IRepository<int, ScheduledEvent> scheduledEventRepository)
        {
            _eventResponseRepository = eventResponseRepository;
            _bookingRepository = bookingRepository; 
            _eventRequestRepository = eventRequestRepository;
            _scheduledEventRepository = scheduledEventRepository;
        }

        public async Task<Booking> MakeBooking(BookingDTO bookingDTO)
        {
            EventResponse response = await _eventResponseRepository.Get(bookingDTO.EventResponseId);
            if(response == null)
            {
                throw new NoSuchEventResponseException();
            }
            if(response.ResponseStatus=="denied")
            {
                throw new ResponseNotAcceptedException();
            }
            if (response.ResponseStatus == "pending")
            {
                throw new ResponseNotAcceptedException();
            }
            Booking booking = new Booking();
            booking.UserId= bookingDTO.UserId;
            booking.Date= DateTime.Now;
            booking.TotalPrice= bookingDTO.TotalPrice;
            booking.Status=bookingDTO.Status;
            booking.PaymentTye= bookingDTO.PaymentTye;
            booking.EventResponseId=response.EventResponseId;
            Booking booking1 = await _bookingRepository.Add(booking);

            EventRequest eventRequest = await _eventRequestRepository.Get(response.EventRequestId);
            ScheduledEvent scheduledEvent = new ScheduledEvent
            {
                UserProfileId = booking.UserId,
                EventResponseId = response.EventResponseId,
                EventId = eventRequest.EventId,
                EventRequestId = eventRequest.EventRequestId,
                IsCompleted=false
            };

            await _scheduledEventRepository.Add(scheduledEvent);

            return booking1;
        }
    }
}
