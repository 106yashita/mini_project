using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<int, Event> _eventRepo;
        private readonly IScheduledEventRepository _scheduledEventRepo;
        public AdminService(IRepository<int, Event> eventRepo, IScheduledEventRepository scheduledEventRepo)
        {
            _eventRepo = eventRepo;
            _scheduledEventRepo = scheduledEventRepo;
        }
        public async Task<Event> CreateEvent(CreateEventDTO eventDTO)
        {
            Event event1 = new Event();
            event1.EventName = eventDTO.EventName;
            event1.EventType = eventDTO.EventType;
            event1.EventDescription = eventDTO.Description;
            event1.location = eventDTO.location;
            event1.Date = DateTime.Now;
            await _eventRepo.Add(event1);
            return event1;
        }

        public async Task<List<Event>> GetAllEvent()
        {
            List<Event> events = (List<Event>)await _eventRepo.GetAll();
            return events;
        }

        public async Task<HashSet<string>> GetEventCategories()
        {
            List<Event> events = (List<Event>)await _eventRepo.GetAll();
            HashSet<string> categories = new HashSet<string>();
            foreach (var item in events)
            {
                categories.Add(item.EventType);
            }
            if (categories.Count > 0)
            {
                return categories;
            }
            throw new NoSuchEventException();
        }

        public async Task<List<ScheduledEventListDTO>> GetUpcomingEvents()
        {
            List<ScheduledEventListDTO> events = await _scheduledEventRepo.GetScheduledEvents();
            return events;
        }

        public async Task UpdateEventDetails(UpdateEventDTO updateEventDTO)
        {
            Event event1 = await _eventRepo.Get(updateEventDTO.EventId);
            if (event1 == null)
            {
                throw new NoSuchEventException();
            }
            event1.EventName = updateEventDTO.EventName;
            event1.EventDescription = updateEventDTO.Description;
            await _eventRepo.Update(event1);
        }

        public async Task UpdateScheduledEvent(int scheduledEventId, bool isCompleted)
        {
            ScheduledEvent scheduledEvent=await _scheduledEventRepo.Get(scheduledEventId);
            if (scheduledEvent == null)
            {
                throw new NoSuchScheduledEventException();
            }
            scheduledEvent.IsCompleted= isCompleted;
            await _scheduledEventRepo.Update(scheduledEvent);
        }

    }  
}
