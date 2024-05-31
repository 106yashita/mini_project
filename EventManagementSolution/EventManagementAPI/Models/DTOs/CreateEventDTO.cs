namespace EventManagementAPI.Models.DTOs
{
    public class CreateEventDTO
    {
        public string EventName { get; set; }
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; }
        public string location { get; set; }
    }
}