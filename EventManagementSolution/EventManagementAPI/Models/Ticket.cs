using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public int SeatNumber { get; set; }
    }
}
