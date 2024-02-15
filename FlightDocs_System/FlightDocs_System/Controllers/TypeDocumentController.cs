using FlightDocs_System.Model;
using FlightDocs_System.Service.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeDocumentController : ControllerBase
    {
        private readonly ITypeDocumentService _service;

        public TypeDocumentController(ITypeDocumentService service)
        {
            _service = service;
        }
        [HttpPost("AddType")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddType(TypeCreate type)
        {
            if(ModelState.IsValid)
            {
                var result = await _service.AddType(type);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");

        }
        [HttpGet("ManagerType")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ManagerType()
        {
            var result = await _service.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetDetail")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _service.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateType")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateType(string id, TypeCreate type)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateType(id, type);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpDelete("InactiveType")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> InactiveType(string id)
        {
            var result = await _service.Inactive(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
