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
    public class EventRequestRepoTest
    {
        private EventManagementContext _context;
        private EventRequestRepository _eventRequestRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _eventRequestRepository = new EventRequestRepository(_context);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Add_Success()
        {
            EventRequest event1 = new EventRequest() 
            { 
                EventRequestId = 1,
                UserId = 1,
                EventId=1,
                Capacity=10,
                location = "gfd",
                DateTime=new DateTime(), 
                EventType = "celebration", 
                RequestedDate = new DateTime(), 
                RequestStatus = "inf",
                EntertainmentDetails="",
                SpecialInstruction="",
                FoodPreferences=""
            };
            var result = _eventRequestRepository.Add(event1);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_Success()
        {
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
             _eventRequestRepository.Add(event1);
            EventRequest event2 = new EventRequest()
            {
                EventRequestId = 2,
                UserId = 2,
                EventId = 2,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            _eventRequestRepository.Add(event2);
            List<EventRequest> requests = _eventRequestRepository.GetAll().Result.ToList();
            //Assert
            Assert.AreEqual(2, requests.Count);
        }

        [Test]
        public void GetAll_Fail()
        {
            List<EventRequest> events = _eventRequestRepository.GetAll().Result.ToList();
            //Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public void Get_Success()
        {
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
             _eventRequestRepository.Add(event1);
            var result = _eventRequestRepository.Get(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Arrange
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            _eventRequestRepository.Add(event1);

            // Act
            var result = await _eventRequestRepository.Get(2);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_Success()
        {
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            _eventRequestRepository.Add(event1);
            var result = _eventRequestRepository.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Fail()
        {
            Assert.ThrowsAsync<NoSuchEventRequestException>(async () => await _eventRequestRepository.Delete(2));
        }
        [Test]
        public void Update_Success()
        {
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            _eventRequestRepository.Add(event1);
            event1.EventType = "birthday";
            var result = _eventRequestRepository.Update(event1).Result;
            Assert.AreEqual("birthday", result.EventType);
        }

        [Test]
        public void Update_Fail()
        {
            EventRequest event1 = new EventRequest()
            {
                EventRequestId = 1,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            _eventRequestRepository.Add(event1);
            EventRequest event2 = new EventRequest()
            {
                EventRequestId = 2,
                UserId = 1,
                EventId = 1,
                Capacity = 10,
                location = "gfd",
                DateTime = new DateTime(),
                EventType = "celebration",
                RequestedDate = new DateTime(),
                RequestStatus = "inf",
                EntertainmentDetails = "",
                SpecialInstruction = "",
                FoodPreferences = ""
            };
            Assert.ThrowsAsync<NoSuchEventRequestException>(async () => await _eventRequestRepository.Update(event2));
        }
    }
}
