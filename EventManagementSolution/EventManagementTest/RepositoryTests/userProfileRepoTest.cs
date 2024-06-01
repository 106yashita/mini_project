using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
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
    public class userProfileRepoTest
    {
        private EventManagementContext _context;
        private UserProfileRepository _userProfileRepository;
        private UserProfile _userProfile;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _userProfileRepository = new UserProfileRepository(_context);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }


        [Test]
        public async Task Add_Success()
        {
            // Arrange
            var user = new UserProfile
            {
                Id = 102,
                UserName = "Johnsnow",
                Email = "john@example.com",
                UserType = "user"
            };

            // Act
            var result = await _userProfileRepository.Add(user);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(102, result.Id);
        }

        [Test]
        public async Task Delete_Success()
        {
            await _userProfileRepository.Add(new UserProfile
            {
                Id = 102,
                UserName = "Johnsnow",
                Email = "john@example.com",
                UserType = "user"
            });

            // Act
            var result = await _userProfileRepository.Delete(102);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(102, result.Id);
        }

        [Test]
        public async Task Delete_Fail()
        {
            // Act & Assert
            Assert.ThrowsAsync<NoSuchUserException>(async () => await _userProfileRepository.Delete(103));
        }

        [Test]
        public async Task Get_Success()
        { 
            var user = new UserProfile
            {
                Id = 103,
                UserName = "Johndfsnow",
                Email = "johnsd@example.com",
                UserType = "user"
            };

            // Act
             await _userProfileRepository.Add(user);
            var result = await _userProfileRepository.Get(103);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Act
            var result = await _userProfileRepository.Get(103);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetAll_Success()
        {
            var user = new UserProfile
            {
                Id = 102,
                UserName = "Johnsnow",
                Email = "john@example.com",
                UserType = "user"
            };

            // Act
            await _userProfileRepository.Add(user);
            // Act
            var result = await _userProfileRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
        }
        [Test]
        public async Task GetAll_Fail()
        {
            // Act
            var result = await _userProfileRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task Update_Success()
        {
            var user = new UserProfile
            {
                Id = 105,
                UserName = "Johndfsnow",
                Email = "johnsd@example.com",
                UserType = "user"
            }; 
            _userProfileRepository.Add(user);
            user.UserType = "admin"; 
            var result = _userProfileRepository.Update(user).Result;
            Assert.AreEqual(user.UserType, result.UserType);
        }
        [Test]
        public async Task Update_Fail()
        {
            // Act
            // Arrange
            var user = new UserProfile
            {
                Id = 104,
                UserName = "Johnsnow",
                Email = "john@example.com",
                UserType = "user"
            };
            // Assert
            Assert.ThrowsAsync<NoSuchUserException>(async () => await _userProfileRepository.Update(user));
        }
    }
}
