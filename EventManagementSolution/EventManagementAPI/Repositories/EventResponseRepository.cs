using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagementAPI.Repositories
{
    public class EventResponseRepository : IRepository<int, EventResponse>
    {
        private readonly EventManagementContext _context;
        public EventResponseRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<EventResponse> Add(EventResponse item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<EventResponse> Delete(int key)
        {
            var response = await Get(key);
            if (response != null)
            {
                _context.Remove(response);
                _context.SaveChangesAsync(true);
                return response;
            }
            throw new NoSuchEventResponseException();
        }

        public async Task<EventResponse> Get(int key)
        {
            var response = await _context.EventResponses.FirstOrDefaultAsync(e => e.EventResponseId == key);
            return response;
        }

        public async Task<IEnumerable<EventResponse>> GetAll()
        {
            var responses = await _context.EventResponses.ToListAsync();
            return responses;
        }

        public async Task<EventResponse> Update(EventResponse item)
        {
            var response = await Get(item.EventResponseId);
            if (response != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return response;
            }
            throw new NoSuchEventResponseException();
        }
    }
}
