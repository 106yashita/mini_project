using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;
using EventManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementTest.ServiceTests
{
    public class BookingServiceTest
    {
        private EventManagementContext _context;
        private EventRepository _eventRepository;
        private EventRequestRepository _eventRequestRepository;
        private EventResponseRepository _eventResponseRepository;
        private ScheduledEventRepository _scheduledEventRepository;
        private EventRequestService _eventRequestService;
        private EventResponseService _eventResponseService;
        private BookingRepository _bookingRepository;
        private BookingService _bookingService;
        private AdminService _adminService;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _eventRepository = new EventRepository(_context);
            _eventRequestRepository = new EventRequestRepository(_context);
            _eventResponseRepository = new EventResponseRepository(_context);
            _scheduledEventRepository = new ScheduledEventRepository(_context);

            _adminService = new AdminService(_eventRepository, _scheduledEventRepository);
            _eventRequestService = new EventRequestService(_eventRequestRepository, _eventRepository);
            _eventResponseService = new EventResponseService(_eventResponseRepository, _eventRequestRepository);
            _bookingService = new BookingService(_eventResponseRepository, _bookingRepository, _eventRequestRepository, _scheduledEventRepository);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void MakeBooking_Success()
        {
            CreateEventDTO createEventDTO = new CreateEventDTO()
            {
                EventName = "birthday",
                Description = "dwey",
                EventType = "ceelbration",
                location = "ctre"
            };
            _adminService.CreateEvent(createEventDTO);
            RequestDTO requestDTO = new RequestDTO()
            {
                UsertId = 1,
                EventId = 1,
                Venue = "sd",
                Capacity = 1,
                Location = "tre",
                EventType = "fewsd",
                FoodPreferences = "fghj",
                EntertainmentDetails = "rtyu",
                SpecialInstructions = "erthjhgfd",
                EventStartDate = DateTime.Now
            };
            _eventRequestService.CreateEventRequest(requestDTO);
            ResponseDTO responseDTO = new ResponseDTO()
            {
                EventRequestId = 1,
                ResponseStatus = "completed",
                Amount = 123,
                ResponseMessage = "werthgfd"
            };
            _eventResponseService.CreateEventResponse(responseDTO);
            BookingDTO bookingDTO = new BookingDTO()
            {
                UserId = 1,
                EventResponseId = 1,
                TotalPrice = 100,
                Status = "OK",
                PaymentTye = "UPI"
            };
            var result= _bookingService.MakeBooking(bookingDTO);
            Assert.IsNotNull(result);
        }
        [Test]
        public void MakeBooking_Fail()
        {
            BookingDTO bookingDTO = new BookingDTO()
            {
                UserId = 1,
                EventResponseId = 1,
                TotalPrice = 100,
                Status = "OK",
                PaymentTye = "UPI"
            };
            Assert.ThrowsAsync<NoSuchEventResponseException>(async () => await _bookingService.MakeBooking(bookingDTO));
        }
    }
}
