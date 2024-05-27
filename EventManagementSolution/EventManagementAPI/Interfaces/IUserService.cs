using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(UserLoginDTO loginDTO);
        public Task<UserProfile> Register(UserRegisterDTO profileDTO);
    }
}
