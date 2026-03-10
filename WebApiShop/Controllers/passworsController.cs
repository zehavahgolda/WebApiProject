using Entity;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        IPasswordService _passwordservice;//

        public PasswordsController(IPasswordService passwordservice)
        {
            _passwordservice = passwordservice;
        }


       

        

        
        [HttpPost]
        public ActionResult<passwordEntity> Post([FromBody] string value)
        {

            passwordEntity resPas = _passwordservice.Level(value);
            if (resPas == null)
                return NoContent();
            return Ok(resPas);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("הסיסמה החדשה לא יכולה להיות ריקה.");
            }

            bool isUpdated = _passwordservice.UpdatePassword(id, newPassword);

            if (isUpdated)
            {
                return Ok($"הסיסמה למשתמש {id} עודכנה בהצלחה.");
            }
            else
            {
                return BadRequest("הסיסמה שנבחרה חלשה מדי. נדרש חוזק של 3 ומעלה.");
            }

           
        }
    }
}