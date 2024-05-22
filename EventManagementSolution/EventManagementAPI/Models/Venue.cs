namespace EventManagementAPI.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int ContactNumber { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
