using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Repositories;

namespace EventManagementAPI.Services
{
    public class EventRequestService : IRequestService
    {
        private readonly IRepository<int, EventRequest> _requestRepository;
        private readonly IRepository<int, Event> _eventRepository;

        public EventRequestService(IRepository<int, EventRequest> eventRequestRepository, IRepository<int, Event> eventRepository)
        {
            _requestRepository = eventRequestRepository;
            _eventRepository = eventRepository;
        }
        public async Task<int> CreateEventRequest(RequestDTO requestDTO)
        {
            Event event1 = await _eventRepository.Get(requestDTO.EventId);

            if (event1 == null)
            {
                throw new NoSuchEventException();
            }

            EventRequest request = new EventRequest();

            request.UserId = requestDTO.UsertId;
            request.EventId = requestDTO.EventId;
            request.Capacity=requestDTO.Capacity;
            request.location = requestDTO.Location;
            request.FoodPreferences = requestDTO.FoodPreferences;
            request.EntertainmentDetails = requestDTO.EntertainmentDetails;
            request.SpecialInstruction = requestDTO.SpecialInstructions;
            request.RequestedDate = requestDTO.EventStartDate;
            request.EventType = requestDTO.EventType;
            request.DateTime=DateTime.Now;
            request.RequestStatus = "pending";
            EventRequest eventRequest =await _requestRepository.Add(request);
            return eventRequest.EventRequestId;
        }

        public async Task<List<EventRequest>> GetAllEventRequest()
        {
            var eventRequests = await _requestRepository.GetAll();
            return eventRequests.ToList();
        }

        public async Task<EventRequest> UpdateRequest(int requestID, string status)
        {
            EventRequest request = await _requestRepository.Get(requestID);
            if(request == null)
            {
                throw new NoSuchEventRequestException();
            }
            request.RequestStatus=status;
            request= await _requestRepository.Update(request);
            return request;
        }
    }
}
