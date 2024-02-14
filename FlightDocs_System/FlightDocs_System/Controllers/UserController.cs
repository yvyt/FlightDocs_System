using FlightDocs_System.Model;
using FlightDocs_System.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocs_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(user);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");
        }
        [HttpPost("Register")]

        public async Task<IActionResult> CreateUser(string role, [FromBody] UserRegister user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Register(role,user);
                if(result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Something is not valid");

        }
    }
}
