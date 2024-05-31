using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IRequestService
    {
        public Task<int> CreateEventRequest(RequestDTO requestDTO);
        public Task<List<EventRequest>> GetAllEventRequest();
        public Task<EventRequest> UpdateRequest(int requestID, string status);
    }
}
