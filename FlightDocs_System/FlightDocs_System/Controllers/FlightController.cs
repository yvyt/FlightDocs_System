using FlightDocs_System.Model;
using FlightDocs_System.Service.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [HttpPost("AddFlight")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateFlight")]
        public async Task<IActionResult> CreateFlight([FromForm] FlightCreate flight)
        {
            if (ModelState.IsValid)
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
        [HttpGet("ManagerFlight")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Back Office")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _flightService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetDetail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewFlight")]


        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _flightService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateFlight")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        [Authorize(Policy = "EditFlight")]
        public async Task<IActionResult> UpdateFlight(string id, [FromForm] FlightCreate flight)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightService.UpdateFlight(id, flight);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpDelete("InactiveFlight")]
        [Authorize(AuthenticationSchemes = "Bearer")]

        [Authorize(Policy = "EditFlight")]
        public async Task<IActionResult> DeleteFlgiht(string id)
        {
            var result = await _flightService.InactiveFlight(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
