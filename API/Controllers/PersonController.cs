using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;

        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAllAsync();

            return Ok(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPerson(PersonModel person)
        {
            var result = await _repo.AddAsync(person);

            if (result == 1)
            {
                return Ok("Add Success");
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpGet("show/{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
            {
                return BadRequest("Not exist");
            }

            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _repo.DeleteAsync(id);

            if (result == 1)
            {
                return Ok("Deleted");
            }
            else
            {
                return BadRequest("Error");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, PersonModel person)
        {

            var result = await _repo.UpdateAsync(id, person);

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok("Updated");
        }
    }
}
