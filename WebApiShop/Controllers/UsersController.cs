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



        [HttpPost("register")]
        public async Task<ActionResult<User>> Post([FromBody] UserDto userDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Phone = userDto.Phone,
                Address = userDto.Address
            };

            User acceptedUser = await _userservice.addUserServices(user);

            if (acceptedUser == null)
            {
                return BadRequest("סיסמה חלשה או משתמש כבר קיים במערכת");
            }

            return Ok(acceptedUser);
        }




        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] User user)
        {
            
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("חובה להזין אימייל וסיסמה");
            }

            _logger.LogInformation($"Attempting login for: Email='{user.Email}'");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User _user = await _userservice.loginServices(user);

            if (_user == null)
            {
                _logger.LogWarning($"Login failed for: {user.Email}");
                return Unauthorized("פרטי התחברות שגויים או משתמש לא קיים");
            }

            _logger.LogInformation($"Login success: UserName={_user.Email}");

            return Ok(_user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            await _userservice.update(userDto, id);
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
