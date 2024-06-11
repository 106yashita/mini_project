using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;

namespace EventManagementAPI.Interfaces
{
    public interface IResponseService
    {
        public Task<int> CreateEventResponse(ResponseDTO responseDTO);
        public Task<EventResponse> UpdateResponse(int responseId,string status);
        public Task<List<EventResponse>> GetAllEventResponse(int userId);
    }
}
