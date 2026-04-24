using eBuyCars.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBuyCars.Api.Controller
{
    [Route(template: "api/user")]
    [ApiController]
    public class UserController : ControllerBase    
    {
        private static List<User> _users = new();
        private static int nextId = 1;

        [HttpGet(template:"all")]

        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }

        [HttpGet(template: "{id}")]

        public IActionResult GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            return Ok(user);
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            user. Id = nextId++;
            user. CreatedAt = DateTime.UtcNow;

            _users.Add(user);

            return Created(uri: $"/api/users/{user.Id}", user);

        }

        [HttpPut(template: "{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            
            return Ok(existingUser);
        }   

        [HttpDelete(template: "{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            _users.Remove(user);
            return NoContent();
        }
    }
}
