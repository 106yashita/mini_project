using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IRequestService _requestService;
        public AdminController(IAdminService adminService, IRequestService requestService)
        {
            _adminService = adminService;
            _requestService = requestService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("events")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateEvent(CreateEventDTO eventDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Event result = await _adminService.CreateEvent(eventDTO);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Unauthorized(new ErrorModel(401, ex.Message));
                }
            }
            else
            {
                return BadRequest("All details are not provided. Please check the object");
            }
        }

        [HttpGet]
        [Route("events")]
        [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEvent()
        {
            try
            {
                List<Event> events = await _adminService.GetAllEvent();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("eventsCategory")]
        [ProducesResponseType(typeof(HashSet<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEventCategeries()
        {
            try
            {

                HashSet<string> events = await _adminService.GetEventCategories();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("events/scheduled")]
        [ProducesResponseType(typeof(List<ScheduledEventListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            try
            {
                List<ScheduledEventListDTO> scheduledEvents = await _adminService.GetUpcomingEvents();
                return Ok(scheduledEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("events")]
        public async Task<IActionResult> UpdateEventDetails(UpdateEventDTO updateEventDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _adminService.UpdateEventDetails(updateEventDTO);
                    return Ok("Event has been Updated");
                }
                return BadRequest("All details are not provided. Please check the object");
            }
           
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("eventsRequest")]
        [ProducesResponseType(typeof(List<EventRequest>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventRequest()
        {
            try
            {
                List<EventRequest> requests=await _requestService.GetAllEventRequest();
                 return Ok(requests);
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [HttpPut] 
        [Authorize(Roles = "admin")]
        [Route("eventsRequestStatus")]
        public async Task<IActionResult> EventRequestStatus(int requestId,string status)
        {
            try
            {
                EventRequest request= await _requestService.UpdateRequest(requestId, status);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "status updated successfully",
                    request
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }


        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("completedEvent")]
        public async Task<IActionResult> UpdateScheduledEvent(int scheduledEventId,bool isCompleted)
        {
            try
            {
                  await _adminService.UpdateScheduledEvent(scheduledEventId,isCompleted);
                  return Ok("Event has been completed");
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

    }
}

