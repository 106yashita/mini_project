using EventManagementAPI.Contexts;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using EventManagementAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace EventManagementTest.RepositoryTests
{
    public class UserRepoTest
    {
        private EventManagementContext _context;
        private UserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        private UserProfile _userProfile;
        private User _user;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _userRepository = new UserRepository(_context);
            _userProfileRepository = new UserProfileRepository(_context);
            // Initialize User
            _userProfile = _userProfileRepository.Add(new UserProfile
            {
                Id = 101,
                UserName = "John",
                Email = "john@example.com",
                UserType = "admin"
            }).Result;

           
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
            var user = new User
            {
                UserProfileId=102,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            };

            // Act
            var result = await _userRepository.Add(user);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(102, result.UserProfileId);
        }

        [Test]
        public async Task Delete_Success()
        {
            _user = _userRepository.Add(new User
            {
                UserProfileId = 101,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            }).Result;

            // Act
            var result = await _userRepository.Delete(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(_user.UserProfileId, result.UserProfileId);
        }

        [Test]
        public async Task Delete_Fail()
        {
            // Act & Assert
            Assert.ThrowsAsync<NoSuchUserException>(async () => await _userRepository.Delete(103));
        }

        [Test]
        public async Task Get_Success()
        {
            _user =(new User
            {
                UserProfileId = 101,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            });
           await _userRepository.Add(_user);
            // Act
            var result = await _userRepository.Get(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(_user.UserProfileId, result.UserProfileId);
        }

        [Test]
        public async Task Get_Fail()
        {
            // Act
            var result = await _userRepository.Get(101);
            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetAll_Success()
        {
            _user = _userRepository.Add(new User
            {
                UserProfileId = 101,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            }).Result;
            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1,result.Count());
        }
        [Test]
        public async Task GetAll_Fail()
        {
            var result = await _userRepository.GetAll();

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task Update_Success()
        {
            // Act
            // Arrange
            var newUser = new User
            {
                UserProfileId = 102,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            };
            await _userRepository.Add(newUser);

            // Modify some data in the user detail
            newUser.Password = new byte[] { 2, 2, 4 };

            // Act
            var result = await _userRepository.Update(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(102,result.UserProfileId);
        }
        [Test]
        public async Task Update_Fail()
        {
            // Act
            // Arrange
            var newUser = new User
            {
                UserProfileId = 103,
                Password = new byte[] { 1, 2, 3 },
                PasswordHashKey = new byte[] { 4, 5, 6 }
            };
            // Assert
            Assert.ThrowsAsync<NoSuchUserException>(async () => await _userRepository.Update(newUser));
        }

    }
}