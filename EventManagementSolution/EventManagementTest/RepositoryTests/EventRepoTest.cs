using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementTest.RepositoryTests
{
    public class EventRepoTest
    {
        private EventManagementContext _context;
        private EventRepository _eventRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _eventRepository = new EventRepository(_context);  
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Add_Success()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            var result = _eventRepository.Add(event1);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_Success()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event1);
            Event event2 = new Event() { EventId = 2, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event2);
            List<Event> events = _eventRepository.GetAll().Result.ToList();
            //Assert
            Assert.AreEqual(2, events.Count);
        }

        [Test]
        public void GetAll_Fail()
        {
            List<Event> events = _eventRepository.GetAll().Result.ToList();

            //Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public void Get_Success()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event1);
            var result = _eventRepository.Get(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Arrange
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            await _eventRepository.Add(event1);

            // Act
            var result = await _eventRepository.Get(2);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_Success()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event1);
            var result = _eventRepository.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Fail()
        {
            Assert.ThrowsAsync<NoSuchEventException>(async () => await _eventRepository.Delete(2));
        }
        [Test]
        public void Update_Success()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event1);
            event1.EventName = "birthday";
            var result = _eventRepository.Update(event1).Result;
            Assert.AreEqual("birthday", result.EventName);
        }

        [Test]
        public void Update_Fail()
        {
            Event event1 = new Event() { EventId = 1, EventName = "wedding", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            _eventRepository.Add(event1);
            Event event2 = new Event() { EventId = 2, EventName = "birthday", EventDescription = "gfd", EventType = "celebration", Date = new DateTime(), location = "inf" };
            Assert.ThrowsAsync<NoSuchEventException>(async () => await _eventRepository.Update(event2));
        }
    }
}
