using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class EventResponse
    {
        public int EventResponseId { get; set; }
        public int EventRequestId { get; set; }
        [ForeignKey("EventRequestId")]
        public EventRequest EventRequest { get; set; }
        public int Amount { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseStatus { get; set;}
        public DateTime ResponseDate { get; set; }
    }
}