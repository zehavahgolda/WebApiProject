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
        passwordservice _passwordservice = new passwordservice();
        // GET: api/<passworsController>
        [HttpGet]
        public void Get(string pass)
        {
            
        }

        // GET api/<passworsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<passworsController>
        [HttpPost]
        public ActionResult<passwordEntity> Post([FromBody] string value)
        {
            passwordEntity _passwordEntity = _passwordservice.Level(value);
            if (_passwordEntity == null)
                return NoContent();
            return Ok(_passwordEntity);
         
        }

        // PUT api/<passworsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<passworsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
