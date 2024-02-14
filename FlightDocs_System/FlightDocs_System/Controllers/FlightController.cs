using FlightDocs_System.Model;
using FlightDocs_System.Service.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService  _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [HttpPost("AddFlight")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<IActionResult> CreateFlight([FromForm] FlightCreate flight)
        {
            if(ModelState.IsValid)
            {
                var result = await _flightService.CreateFlight(flight);
                if (result.IsSuccess)
                {
                    return Ok(result);

                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");


        }

    }
}
