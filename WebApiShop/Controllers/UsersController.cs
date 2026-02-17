using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
using System.Threading.Tasks;
using DTOs;
namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserservice _userservice;
        private ILogger<UsersController> _logger;

        public UsersController(IUserservice userservice, ILogger<UsersController> logger)
        {
            _userservice = userservice;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] User user)
        {
            User acceptedUser = await _userservice.addUserServices(user);

            if (acceptedUser == null)
            {
                return BadRequest("סיסמא חלשה -נסה סיסמא שונה");
            }

            return CreatedAtAction(nameof(Get), new { Id = acceptedUser.Id }, acceptedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] User user)
        {
            _logger.LogInformation($"Attempting login for: Email='{user.Email}', Password='{user.Password}'");
            User _user = await _userservice.loginServices(user);
            if (_user == null)
            {
                return NoContent();
            }
            _logger.LogInformation($"Login success: UserName={_user.Email},passord={_user.Password}");
               
            return Ok(_user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User updatedUser)
        {
            await _userservice.update(updatedUser, id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            UserDto user = await _userservice.GetById(id);
            if (user == null)
                return NoContent();
            return Ok(user);
        }
    }
}
