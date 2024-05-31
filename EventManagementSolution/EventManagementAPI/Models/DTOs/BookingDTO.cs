using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models.DTOs
{
    public class BookingDTO
    {
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentTye { get; set; }
        public int EventResponseId { get; set; }
    }
}
