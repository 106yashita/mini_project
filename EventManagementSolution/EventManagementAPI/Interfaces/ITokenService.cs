using EventManagementAPI.Models;

namespace EventManagementAPI.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(UserProfile userProfile);
    }
}
