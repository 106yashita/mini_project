using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Repositories
{
    public class ScheduledEventRepository : IRepository<int, ScheduledEvent>, IScheduledEventRepository
    {

        private readonly EventManagementContext _context;
        public ScheduledEventRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<ScheduledEvent> Add(ScheduledEvent item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ScheduledEvent> Delete(int key)
        {
            var scheduledEvent = await Get(key);
            if (scheduledEvent != null)
            {
                _context.Remove(scheduledEvent);
                _context.SaveChangesAsync(true);
                return scheduledEvent;
            }
            throw new NoSuchScheduledEventException();
        }

        public async Task<ScheduledEvent> Get(int key)
        {
            var scheduledEvent = await _context.ScheduledEvents.FirstOrDefaultAsync(e => e.ScheduledEventId == key);
            return scheduledEvent;
        }

        public async Task<List<ScheduledEventListDTO>> GetScheduledEvents()
        {
            var scheduledEvents = await _context.ScheduledEvents
                .Select(e => new ScheduledEventListDTO
                {
                    ScheduledEventId = e.ScheduledEventId,
                    EventType = e.Event.EventName,
                    Venue = e.eventRequest.location,
                    Capacity=e.eventRequest.Capacity,
                    EntertainmentDetails=e.eventRequest.EntertainmentDetails,
                    SpecialInstruction = e.eventRequest.SpecialInstruction,
                    FoodPreferences = e.eventRequest.FoodPreferences,
                    EventDate = e.eventRequest.RequestedDate,
                    RequestDate = e.eventRequest.DateTime,
                    IsCompleted = e.IsCompleted

                })
                .OrderBy(e => e.EventDate)
                .ToListAsync();
            return scheduledEvents;
        }

        public async Task<ScheduledEvent> Update(ScheduledEvent item)
        {
            var scheduledEvent = await Get(item.ScheduledEventId);
            if (scheduledEvent != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return scheduledEvent;
            }
            throw new NoSuchScheduledEventException();
        }

        public async Task<IEnumerable<ScheduledEvent>> GetAll()
        {
            var scheduledEvents = await _context.ScheduledEvents.ToListAsync();
            return scheduledEvents;
        }

       
    }
}
