using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IAdminService
    {
        public Task<Event> CreateEvent(CreateEventDTO eventDTO);
        public Task<List<ScheduledEventListDTO>> GetUpcomingEvents();
        public Task UpdateEventDetails(UpdateEventDTO updateEventDTO);
        public Task<List<Event>> GetAllEvent();
        public Task<HashSet<string>> GetEventCategories();
        public Task UpdateScheduledEvent(int  scheduledEventId,bool isCompleted);
    }
}
