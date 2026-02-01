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
        private IUserService _userService;
        private ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] User user)
        {
            User acceptedUser = await _userService.addUserServices(user);

            if (acceptedUser == null)
            {
                return BadRequest("סיסמא חלשה -נסה סיסמא שונה");
            }

            return CreatedAtAction(nameof(Get), new { Id = acceptedUser.Id }, acceptedUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] User user)
        {
            User _user = await _userService.loginServices(user);
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
            await _userService.update(updatedUser, id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            UserDto user = await _userService.GetById(id);
            if (user == null)
                return NoContent();
            return Ok(user);
        }
    }
}
