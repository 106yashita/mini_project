using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace EventManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepo;
        private readonly IRepository<int, UserProfile> _userProfileRepo;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<int, User> userRepo, IRepository<int, UserProfile> userProfileRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _userProfileRepo = userProfileRepo;
            _tokenService = tokenService;
        }
        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var userDB = await _userRepo.Get(loginDTO.UserId);
            if (userDB == null)
            {
                throw new UnauthorizedUserException("Invalid username or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(userDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, userDB.Password);
            if (isPasswordSame)
            {
                var profile = await _userProfileRepo.Get(loginDTO.UserId);
                LoginReturnDTO loginReturnDTO = MapProfileToLoginReturn(profile);
                return loginReturnDTO;
            }
            throw new UnauthorizedUserException("Invalid username or password");
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<UserProfile> Register(UserRegisterDTO profileDTO)
        {
            UserProfile userProfile = null;
            User user = null;
            try
            {
                userProfile = profileDTO;
                user = MapProfileUserDTOToUser(profileDTO);
                userProfile = await _userProfileRepo.Add(userProfile);
                user.UserProfileId = userProfile.Id;
                user = await _userRepo.Add(user);
                ((UserRegisterDTO)userProfile).Password = string.Empty;
                return userProfile;
            }
            catch (Exception) { }
            if (userProfile != null)
                await RevertEmployeeInsert(userProfile);
            if (user != null && userProfile == null)
                await RevertUserInsert(user);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        private LoginReturnDTO MapProfileToLoginReturn(UserProfile profile)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserID = profile.Id;
            returnDTO.Role = profile.UserType ?? "User";
            returnDTO.Token = _tokenService.GenerateToken(profile);
            return returnDTO;
        }

        private async Task RevertUserInsert(User user)
        {
            await _userRepo.Delete(user.UserProfileId);
        }

        private async Task RevertEmployeeInsert(UserProfile user)
        {

            await _userProfileRepo.Delete(user.Id);
        }

        private User MapProfileUserDTOToUser(UserRegisterDTO profileDTO)
        {
            User user = new User();
            user.UserProfileId = profileDTO.Id;
            HMACSHA512 hMACSHA = new HMACSHA512();
            user.PasswordHashKey = hMACSHA.Key;
            user.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(profileDTO.Password));
            return user;
        }



    }
}
