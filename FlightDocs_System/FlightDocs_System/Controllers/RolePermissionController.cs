﻿using FlightDocs_System.Service.RolePermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _service;

        public RolePermissionController(IRolePermissionService service)
        {
            _service = service;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddRolePermission(string RoleId, [FromForm] List<string> permissions)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AddRolePermission(RoleId, permissions);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            } 
            return BadRequest("Something is invalid"); 
        }
    }
}
