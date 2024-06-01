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
    public class EventResponseRepoTest
    {
        private EventManagementContext _context;
        private EventResponseRepository _eventResponseRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _eventResponseRepository = new EventResponseRepository(_context);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Add_Success()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            var result = _eventResponseRepository.Add(event1);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_Success()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);
            EventResponse event2 = new EventResponse()
            {
                EventResponseId = 2,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
             _eventResponseRepository.Add(event2);
            List<EventResponse> requests = _eventResponseRepository.GetAll().Result.ToList();
            //Assert
            Assert.AreEqual(2, requests.Count);
        }

        [Test]
        public void GetAll_Fail()
        {
            List<EventResponse> events = _eventResponseRepository.GetAll().Result.ToList();
            //Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public void Get_Success()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);
            var result = _eventResponseRepository.Get(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Arrange
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);

            // Act
            var result = await _eventResponseRepository.Get(2);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_Success()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);
            var result = _eventResponseRepository.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Fail()
        {
            Assert.ThrowsAsync<NoSuchEventResponseException>(async () => await _eventResponseRepository.Delete(2));
        }
        [Test]
        public void Update_Success()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);
            event1.ResponseMessage = "birthday";
            var result = _eventResponseRepository.Update(event1).Result;
            Assert.AreEqual("birthday", result.ResponseMessage);
        }

        [Test]
        public void Update_Fail()
        {
            EventResponse event1 = new EventResponse()
            {
                EventResponseId = 1,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            _eventResponseRepository.Add(event1);
            EventResponse event2 = new EventResponse()
            {
                EventResponseId = 2,
                EventRequestId = 1,
                Amount = 10,
                ResponseMessage = "celebration",
                ResponseDate = new DateTime(),
                ResponseStatus = "inf",
            };
            Assert.ThrowsAsync<NoSuchEventResponseException>(async () => await _eventResponseRepository.Update(event2));
        }
    }
}
