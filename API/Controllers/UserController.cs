using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _repo.Login(model);

            if (token == null)
            {
                return BadRequest("Error.");
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _repo.Register(model);

            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }

            return Ok("User is created.");
        }
    }
}
