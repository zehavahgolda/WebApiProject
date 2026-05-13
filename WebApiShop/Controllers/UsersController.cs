using DTOs;
using Entity;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System.Threading.Tasks;
using System.Security.Claims; 

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserservice _userservice;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserservice userservice, ILogger<UsersController> logger)
        {
            _userservice = userservice;
            _logger = logger;
        }
      

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Post([FromBody] UserRegisterDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            User user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password,
                Phone = userDto.Phone,
                Address = userDto.Address
            };

            _logger.LogInformation($"Registering new user: {user.Email}");
            User acceptedUser = await _userservice.addUserServices(user);

            if (acceptedUser == null)
            {
                _logger.LogWarning($"Registration failed for: {user.Email}");
                return BadRequest("סיסמה חלשה או משתמש כבר קיים במערכת");
            }

            return Ok(new UserResponseDto(acceptedUser.Id, acceptedUser.FirstName, acceptedUser.LastName, acceptedUser.Email, acceptedUser.Phone, acceptedUser.Address));
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] UserLoginDto loginInfo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation($"Attempting login for: Email='{loginInfo.Email}'");
            var result = await _userservice.loginServices(loginInfo);

            if (result == null)
            {
                _logger.LogWarning($"Login failed for: {loginInfo.Email}");
                return Unauthorized("פרטי התחברות שגויים או משתמש לא קיים");
            }

            Response.Cookies.Append("authToken", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = System.DateTime.UtcNow.AddHours(3)
            });

            _logger.LogInformation($"Login success: UserName={result.User.Email}");
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] UserRegisterDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _userservice.update(userDto, id);
            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserResponseDto>> Get(int id)
        {
            
            var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken != id.ToString())
            {
                return Forbid();
            }
            var user = await _userservice.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

    }
}