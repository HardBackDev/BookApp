using BookAppServer.Contracts.ServicesContracts;
using BookAppServer.Dto.UserDto;
using BookAppServer.Filters;
using BookAppServer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAppServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.UserService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForLogin user)
        {
            if (!await _service.UserService.ValidateUser(user))
                return Unauthorized();
            var token = await _service.UserService.CreateToken();
            
            return Ok(new { token = token.token });
        }

        [HttpGet("Privacy")]
        [Authorize]
        public async Task<IActionResult> GetPrivacy()
        {
            var claims = User.Claims
                .Select(x => new {x.Type, x.Value})
                .ToList();

            Console.WriteLine(claims);
            return Ok(claims);
        }
    }
}
