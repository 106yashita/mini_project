using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagementAPI.Repositories
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventManagementContext _context;
        public EventRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<Event> Add(Event item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Event> Delete(int key)
        {
            var Event = await Get(key);
            if (Event != null)
            {
                _context.Remove(Event);
                _context.SaveChangesAsync(true);
                return Event;
            }
            throw new NoSuchEventException();
        }

        public async Task<Event> Get(int key)
        {
            var Event = await _context.Events.FirstOrDefaultAsync(e=> e.EventId==key);
            return Event;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            return events;
        }

        public async Task<Event> Update(Event item)
        {
            var Event = await Get(item.EventId);
            if (Event != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return Event;
            }
            throw new NoSuchEventException();
        }
    }
}
