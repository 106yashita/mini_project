using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Contexts
{
    public class EventManagementContext : DbContext
    {
        public EventManagementContext(DbContextOptions<EventManagementContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventRequest> EventRequests { get; set; }
        public DbSet<EventResponse> EventResponses { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
    }
}
