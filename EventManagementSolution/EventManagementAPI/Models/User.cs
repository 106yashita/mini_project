using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class User
    {
        [Key]
        public int UserProfileId { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordHashKey { get; set; }

        [ForeignKey("UserProfileId")]
        public UserProfile userProfile { get; set; }

        public ICollection<EventRequest> eventRequests { get; set; }

    }
}
