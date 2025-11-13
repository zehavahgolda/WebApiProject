using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
//using WebApiShop.Models;
using Services;
using Entity;
using Repository;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _filePath = "ListOfUsers.txt";
        Userservice _userservice=new Userservice();

        public UsersController()
        {
            if (!System.IO.File.Exists(_filePath))
                System.IO.File.Create(_filePath).Close();
        }

        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {

            User acceptedUser = _userservice.addUserServices(user);

            if (acceptedUser == null)
            {
                return BadRequest("סיסמא חלשה -נסה סיסמא שונה");
            }
            return CreatedAtAction(nameof(Get), new { Id = acceptedUser.id }, acceptedUser);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            user= _userservice.loginServices(user);
            if (user == null)
                return NoContent();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User updatedUser)
        {
             _userservice.update(updatedUser, id);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _userservice.GetUserByidService(id);
            if (user == null)  
                return NoContent();
            return Ok(user);
 
        }
       
        
    }
}
