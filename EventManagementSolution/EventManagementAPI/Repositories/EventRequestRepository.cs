using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagementAPI.Repositories
{
    public class EventRequestRepository : IRepository<int, EventRequest>
    {
        private readonly EventManagementContext _context;
        public EventRequestRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<EventRequest> Add(EventRequest item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<EventRequest> Delete(int key)
        {
            var request = await Get(key);
            if (request != null)
            {
                _context.Remove(request);
                _context.SaveChangesAsync(true);
                return request;
            }
            throw new NoSuchEventRequestException();
        }

        public async Task<EventRequest> Get(int key)
        {
            var request = await _context.EventRequests.FirstOrDefaultAsync(e => e.EventRequestId == key);
            return request;
        }

        public async Task<IEnumerable<EventRequest>> GetAll()
        {
            var requests = await _context.EventRequests.ToListAsync();
            return requests;
        }

        public async Task<EventRequest> Update(EventRequest item)
        {
            var request = await Get(item.EventRequestId);
            if (request != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return request;
            }
            throw new NoSuchEventRequestException();
        }
    }
}
