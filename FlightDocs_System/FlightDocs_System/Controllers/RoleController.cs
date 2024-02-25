using FlightDocs_System.Model;
using FlightDocs_System.Service.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }
        [HttpPost("AddRole")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "CreateRole")]

        public async Task<IActionResult> AddRole(RoleCreate role)
        {
            if(ModelState.IsValid)
            {
                var result = await _service.AddRole(role);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is invalid");
        }
        [HttpGet("ManagerRole")]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin")]

        public async Task<IActionResult> ManagerRole()
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
        [Authorize(Policy = "ViewRole")]

        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _service.GetById(id);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("EditRole")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Policy = "EditRole")]
        public async Task<IActionResult> EditRole(string id, RoleCreate role)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.EditRole(id, role);
                if(result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is invalid");
        }
    }
}
