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
    public class UserServiceTest
    {
        private EventManagementContext _context;
        private UserRepository _userRepository;
        private UserProfileRepository _userProfileRepository;
        private UserService _userService;
        private ITokenService _tokenService;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EventManagementContext>()
                .UseInMemoryDatabase(databaseName: "dummyDB")
                .Options;
            _context = new EventManagementContext(options);
            _userRepository = new UserRepository(_context);
            _userProfileRepository = new UserProfileRepository(_context);

            Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
            configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            Mock<IConfigurationSection> configTokenSection = new Mock<IConfigurationSection>();
            configTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(configTokenSection.Object);
            _tokenService = new TokenService(mockConfig.Object);
            _userService = new UserService(_userRepository,_userProfileRepository,_tokenService);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Register_Success()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                UserName = "John Doe",
                Email = "john@example.com",
                UserType = "admin",
                Password = "password"

            };

            // Act
            var result =  _userService.Register(userRegisterDTO).Result;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userRegisterDTO.UserName, result.UserName);
            Assert.AreEqual(userRegisterDTO.Email, result.Email);
        }
        [Test]
        public void RegisterRevertUser_Fail()
        {
            var userRegisterDTO = new UserRegisterDTO
            {
                // Missing required fields
                Email = "john@example.com",
                Password = "password"
            };


            // Act & Assert
            Assert.ThrowsAsync<NoSuchUserException>(async () => await _userService.Register(userRegisterDTO));
        }

        [Test]
        public void RegisterException_Fail()
        {
            // Arrange
            var userRegisterDTO1 = new UserRegisterDTO
            {
                UserName = "John Doe",
                Email = "john@example.com",
                UserType = "admin",
                Password = "password"

            };
             _userService.Register(userRegisterDTO1);
            var userRegisterDTO2 = new UserRegisterDTO
            {
                UserName = "John Doe",
                Email = "john@example.com",
                UserType = "admin",
                Password = "password"

            };
            Assert.ThrowsAsync<UnableToRegisterException>(async () => await _userService.Register(userRegisterDTO2));

        }
        [Test]
        public async Task LoginSuccessTest()
        {
            //Arrange
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                UserName = "John Doe",
                Email = "john@example.com",
                UserType = "admin",
                Password = "password"

            };
            _userService.Register(userRegisterDTO);

            // Act

            var userLoginDTO = new UserLoginDTO
            {
               UserId = 101,
                Password = "password"
            };

            // Act
            var result = await _userService.Login(userLoginDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userLoginDTO.UserId,result.UserID);

        }

        [Test]
        public async Task Login_Fail()
        {
            // Act

            var userLoginDTO = new UserLoginDTO
            {
                UserId = 101,
                Password = "password"
            };

            // Act
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _userService.Login(userLoginDTO));

        }

        [Test]
        public async Task LoginWrongPassword_Test()
        {
            //Arrange
            // Arrange
            var userRegisterDTO = new UserRegisterDTO
            {
                UserName = "John Doe",
                Email = "john@example.com",
                UserType = "admin",
                Password = "password"

            };
            _userService.Register(userRegisterDTO);

            // Act

            var userLoginDTO = new UserLoginDTO
            {
                UserId = 101,
                Password = "passwo"
            };

            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _userService.Login(userLoginDTO));


        }
    }
}
