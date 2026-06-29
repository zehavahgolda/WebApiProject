using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApiShop.Models;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _filePath = "ListOfUsers.txt";

        public UsersController()
        {
            if (!System.IO.File.Exists(_filePath))
                System.IO.File.Create(_filePath).Close();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User postData)
        {
            int numberOfUsers = System.IO.File.ReadLines(_filePath).Count();
            postData.id = numberOfUsers + 1;

            string userJson = JsonSerializer.Serialize(postData);
            await System.IO.File.AppendAllTextAsync(_filePath, userJson + Environment.NewLine); // fix(C5): async

            return CreatedAtAction(nameof(Get), new { id = postData.id }, postData);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User postData)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath); // fix(C5): use async file I/O

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var user = JsonSerializer.Deserialize<User>(line);
                if (user.userName == postData.userName && user.password == postData.password)
                    return Ok(user);
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            var lines = System.IO.File.ReadAllLines(_filePath).ToList();
            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var user = JsonSerializer.Deserialize<User>(lines[i]);
                if (user.id == id)
                {
                    lines[i] = JsonSerializer.Serialize(updatedUser);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                await System.IO.File.WriteAllLinesAsync(_filePath, lines); // fix(C5): async
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var lines = await System.IO.File.ReadAllLinesAsync(_filePath); // fix(C5): use async file I/O
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var user = JsonSerializer.Deserialize<User>(line);
                if (user.id == id)
                    return Ok(user);
            }
            return NotFound();
        }
    }
}
