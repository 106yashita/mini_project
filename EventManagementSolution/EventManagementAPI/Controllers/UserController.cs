using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "user")]
    public class UserController : ControllerBase
    {
        private readonly IResponseService _responseService;
        public UserController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpPut]
        [Route("eventsResponseStatus")]
        public async Task<IActionResult> EventResponseStatus(int responseId, string status)
        {
            try
            {
                EventResponse response = await _responseService.UpdateResponse(responseId, status);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    Message = "status updated successfully",
                    response
                });
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }
        [HttpGet]
        [Route("eventsResponse")]
        [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventResponse()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.Name);
                List<EventResponse> requests = await _responseService.GetAllEventResponse(Convert.ToInt32(userId));
                return Ok(requests);
            }

            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }
    }
}
