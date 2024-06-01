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
    public class BookingRepoTest
    {
        private EventManagementContext _context;
        private BookingRepository _bookingRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _bookingRepository = new BookingRepository(_context);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Add_Success()
        {
            Booking booking = new Booking()
            {
                BookingId=1,
                Date=new DateTime(),
                TotalPrice= 1000,
                EventResponseId = 1,
                UserId = 1,
                Status="completed",
                PaymentTye="UPI"
            };
            var result = _bookingRepository.Add(booking);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetAll_Success()
        {
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking);
            Booking booking1 = new Booking()
            {
                BookingId = 2,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking1);
            List<Booking> requests = _bookingRepository.GetAll().Result.ToList();
            //Assert
            Assert.AreEqual(2, requests.Count);
        }

        [Test]
        public void GetAll_Fail()
        {
            List<Booking> events = _bookingRepository.GetAll().Result.ToList();
            //Assert
            Assert.IsEmpty(events);
        }

        [Test]
        public void Get_Success()
        {
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking);
            var result = _bookingRepository.Get(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Arrange
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
             _bookingRepository.Add(booking);

            // Act
            var result = await _bookingRepository.Get(2);

            // Assert
            Assert.IsNull(result);
        }


        [Test]
        public void Delete_Success()
        {
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking);
            var result = _bookingRepository.Delete(1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Fail()
        {
            Assert.ThrowsAsync<NoSuchBookingException>(async () => await _bookingRepository.Delete(2));
        }
        [Test]
        public void Update_Success()
        {
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking);
            booking.Status = "canceled";
            var result = _bookingRepository.Update(booking).Result;
            Assert.AreEqual("canceled", result.Status);
        }

        [Test]
        public void Update_Fail()
        {
            Booking booking = new Booking()
            {
                BookingId = 1,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            _bookingRepository.Add(booking);
            Booking booking1 = new Booking()
            {
                BookingId = 2,
                Date = new DateTime(),
                TotalPrice = 1000,
                EventResponseId = 1,
                UserId = 1,
                Status = "completed",
                PaymentTye = "UPI"
            };
            Assert.ThrowsAsync<NoSuchBookingException>(async () => await _bookingRepository.Update(booking1));
        }

    }
}
