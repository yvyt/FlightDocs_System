using FlightDocs_System.Model;
using FlightDocs_System.Service.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Http.Headers;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDocumentController : ControllerBase
    {
        private readonly IFlightDocumentService _service;

        public FlightDocumentController(IFlightDocumentService service)
        {
            _service = service;
        }

        [HttpPost("AddFlightDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddFlightDocument([FromForm] FlightDocumentCreate fd)
        {
            if(ModelState.IsValid)
            {
                var result = await _service.AddFlightDocuments(fd);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest("Something is invalid");
        }
        [HttpGet("ManagerFlightDocuments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ManagerFlightDocuments()
        {
            var result = await _service.GetAll();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetDetail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await _service.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateFlightDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateFlightDocument(string id, [FromForm ]FlightDocumentUpdate fd)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateFlightDocument(id, fd);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is invalid");

        }
        [HttpDelete("InactiveDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> InactiveDocument(string id)
        {
            var result = await _service.InActive(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("DownloadFlightDocument")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> DownloadFD(string id)
        {
            var (fileStream, message) = await _service.DownloadFD(id);
            if (fileStream != null)
            {
                var contentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = message,
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                return File(fileStream, "application/octet-stream", message);
            }
            return BadRequest(message);
        }
    }
}
