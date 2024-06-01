using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IScheduledEventRepository : IRepository<int,ScheduledEvent>
    {
        public Task<List<ScheduledEventListDTO>> GetScheduledEvents();
    }
}
