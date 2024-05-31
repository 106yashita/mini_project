using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class ScheduledEvent
    {
        public int ScheduledEventId { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int EventResponseId { get; set; }
        [ForeignKey("EventResponseId")]
        public EventResponse eventResponse { get; set; }
        public int EventRequestId { get; set; }
        [ForeignKey("EventRequestId")]
        public EventRequest eventRequest { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public UserProfile userProfile { get; set; }
        
    }
}
