namespace EventManagementAPI.Models.DTOs
{
    public class UpdateEventDTO
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
    }
}