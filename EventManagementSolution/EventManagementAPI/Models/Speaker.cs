using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Speaker
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }

    }
}
