using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;

namespace EventManagementAPI.Services
{
    public class EventResponseService : IResponseService
    {
        private readonly IRepository<int, EventResponse> _eventResponsRepository;
        private readonly IRepository<int, EventRequest> _eventRequestRepository;
        public EventResponseService(IRepository<int, EventResponse> eventResponseRepository, IRepository<int, EventRequest> eventRequestRepository)
        {
            _eventResponsRepository = eventResponseRepository;
            _eventRequestRepository = eventRequestRepository;
        }
        public async Task<int> CreateEventResponse(ResponseDTO responseDTO)
        {
            EventRequest eventRequest = await _eventRequestRepository.Get(responseDTO.EventRequestId);

            if (eventRequest == null)
            {
                throw new NoSuchEventRequestException();
            }
            if(eventRequest.RequestStatus == "decline")
            {
                throw new RequestNotAcceptedException();
            }
            EventResponse eventResponse = new EventResponse();
            eventResponse.EventRequestId = responseDTO.EventRequestId;
            eventResponse.Amount = responseDTO.Amount;
            eventResponse.ResponseMessage = responseDTO.ResponseMessage;
            eventResponse.ResponseStatus = "pending";
            eventRequest.RequestStatus = "responded";

            await _eventRequestRepository.Update(eventRequest);
            EventResponse response= await _eventResponsRepository.Add(eventResponse);
            return response.EventResponseId;
        }

        public async Task<List<EventResponse>> GetAllEventResponse(int userId)
        {
            var eventResponses = await _eventResponsRepository.GetAll();
            List<EventResponse> responses = new List<EventResponse>();
            foreach (var eventResponse in eventResponses)
            {
               EventRequest request= await _eventRequestRepository.Get(eventResponse.EventRequestId);
                if(request.UserId == userId)
                {
                    responses.Add(eventResponse);
                }
            }
            return responses.ToList();
        }

        public async Task<EventResponse> UpdateResponse(int responseId, string status)
        {
            EventResponse response = await _eventResponsRepository.Get(responseId);
            if(response == null)
            {
                throw new NoSuchEventResponseException();
            }
            response.ResponseStatus = status;
            response= await _eventResponsRepository.Update(response);

            return response;
        }
    }
}
