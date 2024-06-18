using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using EventManagementAPI.Models.DTOs;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class EventController : ControllerBase
    {
        private readonly IRequestService _eventRequestService;
        private readonly IResponseService _eventResponseService;

        public EventController(IRequestService eventRequestService, IResponseService eventResponseService)
        {
            _eventRequestService = eventRequestService;
            _eventResponseService = eventResponseService;
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        [Route("request")]
        public async Task<IActionResult> CreateEventRequest(RequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int RequestId = await _eventRequestService.CreateEventRequest(requestDTO);
                    return StatusCode(StatusCodes.Status201Created, new
                    {
                        Message = "Request created successfully",
                        RequestId
                    });
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
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("response")]
        public async Task<IActionResult> CreateEventResponse(ResponseDTO responseDTO)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    int ResponseId = await _eventResponseService.CreateEventResponse(responseDTO);
                    return StatusCode(StatusCodes.Status201Created, new
                    {
                        Message = " Request Responded successfully",
                        ResponseId
                    });
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
    }
}
