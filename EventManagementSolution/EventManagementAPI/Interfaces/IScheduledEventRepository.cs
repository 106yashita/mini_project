using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IScheduledEventRepository : IRepository<int,ScheduledEvent>
    {
        public Task<List<ScheduledEventListDTO>> GetScheduledEvents();
        public Task<ScheduledEvent> GetScheduledEventByEventResponseId(int eventResponseId);
        public Task<ScheduledEvent> GetScheduledEventByUserId(int userId);
        public Task<List<ScheduledEventListDTO>> GetUserScheduledEvents(int userId);
    }
}
