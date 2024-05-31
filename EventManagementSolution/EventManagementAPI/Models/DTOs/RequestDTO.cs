using System;

namespace EventManagementAPI.Models.DTOs
{
    public class RequestDTO
    {
        public int UsertId { get; set; }
        public int EventId { get; set; }
        public string Venue { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public string EventType { get; set; }
        public string FoodPreferences { get; set; }  
        public string EntertainmentDetails { get; set; }
        public string SpecialInstructions { get; set; }
        public DateTime EventStartDate { get; set; }
    }
}
