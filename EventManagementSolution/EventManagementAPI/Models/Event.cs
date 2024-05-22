using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventType { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int VenueId { get; set; }
        [ForeignKey("VenueId")]
        public Venue venue { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Speaker> Speakers { get; set; }

    }
}
