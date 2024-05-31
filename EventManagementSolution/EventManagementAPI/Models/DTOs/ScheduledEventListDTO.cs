using System;

namespace EventManagementAPI.Models.DTOs
{
    public class ScheduledEventListDTO
    {
        public int ScheduledEventId { get; set; }
        public string EventType { get; set; }
        public string Venue { get; set; }
        public int Capacity { get; set; }
        public string EntertainmentDetails { get; set; }
        public string SpecialInstruction { get; set; }
        public string FoodPreferences { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
