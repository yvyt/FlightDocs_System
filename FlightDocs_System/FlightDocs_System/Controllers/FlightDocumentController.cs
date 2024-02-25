using FlightDocs_System.Model;
using FlightDocs_System.Service.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO.Compression;
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
        [Authorize(Policy = "CreateFlightDocument")]

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
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Back Office")]
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
        [Authorize(Policy = "ViewFlightDocument")]

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
        [Authorize(Policy = "EditFlightDocument")]

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
        [Authorize(Policy = "EditFlightDocument")]

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
        [Authorize(Policy = "ViewFlightDocument")]

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
        [HttpGet("GetByFlight")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewFlightDocument")]

        public async Task<IActionResult> GetByFlight(string flightID)
        {
            var result = await _service.GetByFlight(flightID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetByType")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "ViewFlightDocument")]

        public async Task<IActionResult> GetByType(string typeID)
        {
            var result = await _service.GetByType(typeID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("DownloadZip")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Authorize(Policy = "ViewFlightDocument")]

        public async Task<IActionResult> DownloadZip(string flightID)
        {
           var zipFile = await _service.DownloadZip(flightID);
            return File(zipFile, "application/zip", "download.zip");            
        }
        [HttpGet("ConfirmDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditFlightDocument")]

        public async Task<IActionResult> ConfirmDocument(string id)
        {
            var result = await _service.ConfirmDocument(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
 