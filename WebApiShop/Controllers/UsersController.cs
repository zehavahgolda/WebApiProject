using Microsoft.AspNetCore.Mvc;
using Services;
using Repository.Models;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserservice _userservice;

        public UsersController(IUserservice userservice)
        {
            _userservice = userservice;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            User acceptedUser = await _userservice.addUserServices(user);

            if (acceptedUser == null)
            {
                return BadRequest("סיסמא חלשה -נסה סיסמא שונה");
            }

            return CreatedAtAction(nameof(Get), new { Id = acceptedUser.Id }, acceptedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User loggedUser = await _userservice.loginServices(user);
            if (loggedUser == null)
                return NoContent();
            return Ok(loggedUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User updatedUser)
        {
            await _userservice.update(updatedUser, id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _userservice.GetUserByidService(id);
            if (user == null)
                return NoContent();
            return Ok(user);
        }
    }
}
