using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;
using EventManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementTest.ServiceTests
{
    public class AdminServiceTest
    {
        private EventManagementContext _context;
        private EventRepository _eventRepository;
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
            _scheduledEventRepository = new ScheduledEventRepository(_context);

            _adminService = new AdminService(_eventRepository, _scheduledEventRepository);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void CreateEvent_Test()
        {
            CreateEventDTO createEventDTO = new CreateEventDTO()
            {
                EventName="birthday",
                Description="dwey",
                EventType="ceelbration",
                location="ctre"
            };
            var result= _adminService.CreateEvent(createEventDTO).Result;
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllEvent_Test()
        {
            CreateEventDTO createEventDTO = new CreateEventDTO()
            {
                EventName = "birthday",
                Description = "dwey",
                EventType = "ceelbration",
                location = "ctre"
            };
            _adminService.CreateEvent(createEventDTO);
            var result = _adminService.GetAllEvent();
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Result.Count());
        }

        [Test]
        public void GetEventCategories_Test()
        {
            CreateEventDTO createEventDTO = new CreateEventDTO()
            {
                EventName = "birthday",
                Description = "dwey",
                EventType = "ceelbration",
                location = "ctre"
            };
            _adminService.CreateEvent(createEventDTO);
            var result = _adminService.GetEventCategories().Result;
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetEventCategories_Fail()
        {
            Assert.ThrowsAsync<NoSuchEventException>(async () => await _adminService.GetEventCategories());

        }

        [Test]
        public void GetScheduledEvent_Success()
        {
            var result=_adminService.GetUpcomingEvents().Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

        }
        [Test]
        public void UpdateEvent_Success()
        {
            CreateEventDTO createEventDTO = new CreateEventDTO()
            {
                EventName = "birthday",
                Description = "dwey",
                EventType = "ceelbration",
                location = "ctre"
            };
            _adminService.CreateEvent(createEventDTO);
            UpdateEventDTO updateEventDTO = new UpdateEventDTO()
            {
                EventId = 1,
                EventName = "birthday",
                Description = "tre"
            };
            var result = _adminService.UpdateEventDetails(updateEventDTO);
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateEvent_Fail()
        {
            UpdateEventDTO updateEventDTO = new UpdateEventDTO()
            {
                EventId = 1,
                EventName = "birthday",
                Description = "tre"
            };
            Assert.ThrowsAsync<NoSuchEventException>(async () => await _adminService.UpdateEventDetails(updateEventDTO));
        }
        [Test]
        public void UpdateScheduledEvent_Test()
        {
            Assert.ThrowsAsync<NoSuchScheduledEventException>(async () => await _adminService.UpdateScheduledEvent(1,true));
        }
    }
}
