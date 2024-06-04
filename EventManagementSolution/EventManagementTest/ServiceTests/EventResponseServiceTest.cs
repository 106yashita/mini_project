using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;
using EventManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementTest.ServiceTests
{
    public class EventResponseServiceTest
    {
        private EventManagementContext _context;
        private EventRepository _eventRepository;
        private EventRequestRepository _eventRequestRepository;
        private EventResponseRepository _eventResponseRepository;
        private EventResponseService _eventResponseService;
        private EventRequestService _eventRequestService;
        private ScheduledEventRepository _scheduledEventRepository;
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
            _eventResponseService = new EventResponseService(_eventResponseRepository,_eventRequestRepository);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
       public void CreateEventResponse_Success()
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
            var result= _eventResponseService.CreateEventResponse(responseDTO).Result;
            Assert.IsNotNull(result);
        }
        [Test]
        public void CreateEventResponse_Fail()
        {
            ResponseDTO responseDTO = new ResponseDTO()
            {
                EventRequestId = 1,
                ResponseStatus = "completed",
                Amount = 123,
                ResponseMessage = "werthgfd"
            };
            Assert.ThrowsAsync<NoSuchEventRequestException>(async () => await _eventResponseService.CreateEventResponse(responseDTO));
        }
        [Test]
        public void CreateEventResponse_RequestNotAccepted()
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
            _eventRequestService.UpdateRequest(1, "decline");
            ResponseDTO responseDTO = new ResponseDTO()
            {
                EventRequestId = 1,
                ResponseStatus = "completed",
                Amount = 123,
                ResponseMessage = "werthgfd"
            };
            Assert.ThrowsAsync<RequestNotAcceptedException>(async () => await _eventResponseService.CreateEventResponse(responseDTO));
        }
        [Test]
        public void UpdateResponse_Success()
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
            var result = _eventResponseService.UpdateResponse(1, "accepted");
            Assert.IsNotNull(result);
        }
        [Test]
        public void UpdateEventResponse_Fail()
        {
            Assert.ThrowsAsync<NoSuchEventResponseException>(async () => await _eventResponseService.UpdateResponse(1,"accepted"));
        }
    }
}
