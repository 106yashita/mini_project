using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
