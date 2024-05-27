using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? UserType { get; set; }
    }
}
