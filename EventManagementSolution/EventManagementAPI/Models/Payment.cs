using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public int TransctionId { get; set; }
    }
}
