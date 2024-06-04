using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
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
    public class EventRequestServiceTest
    {
        private EventManagementContext _context;
        private EventRepository _eventRepository;
        private EventRequestRepository _eventRequestRepository;
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
            _scheduledEventRepository = new ScheduledEventRepository(_context);

            _adminService = new AdminService(_eventRepository, _scheduledEventRepository);
            _eventRequestService = new EventRequestService(_eventRequestRepository,_eventRepository);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void CreateEventRequest_Test()
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
                UsertId=1,
                EventId=1,
                Venue="sd",
                Capacity=1,
                Location="tre",
                EventType="fewsd",
                FoodPreferences="fghj",
                EntertainmentDetails="rtyu",
                SpecialInstructions="erthjhgfd",
                EventStartDate=DateTime.Now
            };
            var result = _eventRequestService.CreateEventRequest(requestDTO).Result;
            Assert.IsNotNull(result);
        }
        [Test]
        public void CreateEventRequest_TestFail()
        {
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
            Assert.ThrowsAsync<NoSuchEventException>(async () => await _eventRequestService.CreateEventRequest(requestDTO));
        }

        [Test]
        public void GetAllEventRequest_Test()
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
            var result=  _eventRequestService.GetAllEventRequest().Result;
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void UpdateEventRequest_Test()
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
            var result = _eventRequestService.UpdateRequest(1, "done");
            Assert.IsNotNull(result);
        }
        [Test]
        public void UpdateEventRequest_TestFail()
        {
            Assert.ThrowsAsync<NoSuchEventRequestException>(async () => await _eventRequestService.UpdateRequest(1, "done"));
        }
    }
}
