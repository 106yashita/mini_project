using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentTye { get; set; }
        public int EventResponseId { get; set; }
        [ForeignKey("EventResponseId")]
        public EventResponse eventResponse { get; set; }
    }
}
