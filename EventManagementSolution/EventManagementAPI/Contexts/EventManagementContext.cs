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
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Venue> Venues { get; set; }


    }
}
