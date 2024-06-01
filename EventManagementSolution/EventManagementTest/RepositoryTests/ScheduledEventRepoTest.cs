using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementTest.RepositoryTests
{
    public class ScheduledEventRepoTest
    {
        private EventManagementContext _context;
        private ScheduledEventRepository _scheduledEventRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _scheduledEventRepository = new ScheduledEventRepository(_context);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Add_Success()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId=1,
                EventId=1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted=true,
                UserProfileId=1
            };
            var result = _scheduledEventRepository.Add(event1);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_Success()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            ScheduledEvent event2 = new ScheduledEvent()
            {
                ScheduledEventId = 2,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event2);
            List<ScheduledEvent> requests = _scheduledEventRepository.GetAll().Result.ToList();
            //Assert
            Assert.AreEqual(2, requests.Count);
        }

        [Test]
        public void GetAll_Fail()
        {
            List<ScheduledEvent> events = _scheduledEventRepository.GetAll().Result.ToList();
            //Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public void Get_Success()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            var result = _scheduledEventRepository.Get(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Arrange
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
             _scheduledEventRepository.Add(event1);

            // Act
            var result = await _scheduledEventRepository.Get(2);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_Success()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            var result = _scheduledEventRepository.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Fail()
        {
            Assert.ThrowsAsync<NoSuchScheduledEventException>(async () => await _scheduledEventRepository.Delete(2));
        }
        [Test]
        public void Update_Success()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            event1.IsCompleted = false;
            var result = _scheduledEventRepository.Update(event1).Result;
            Assert.AreEqual(false, result.IsCompleted);
        }

        [Test]
        public void Update_Fail()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            ScheduledEvent event2 = new ScheduledEvent()
            {
                ScheduledEventId = 2,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            Assert.ThrowsAsync<NoSuchScheduledEventException>(async () => await _scheduledEventRepository.Update(event2));
        }

        [Test]
        public void GetScheduledEvent()
        {
            ScheduledEvent event1 = new ScheduledEvent()
            {
                ScheduledEventId = 1,
                EventId = 1,
                EventResponseId = 1,
                EventRequestId = 1,
                IsCompleted = true,
                UserProfileId = 1
            };
            _scheduledEventRepository.Add(event1);
            var result =  _scheduledEventRepository.GetScheduledEvents().Result;
            Assert.AreEqual(0, result.Count);
        }
    }
}
