using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class EventRequest
    {
        public int EventRequestId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserProfile userProfile { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int Capacity { get; set; }
        public string location { get; set; }
        public DateTime DateTime { get; set; }
        public string EventType { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestStatus { get; set; }
        public string EntertainmentDetails { get; set; }
        public string SpecialInstruction { get; set; }
        public string FoodPreferences { get; set; }

    }
}
