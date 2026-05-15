using BuyCars.Api.Filters;
using BuyCars.BusinessLogic;
using BuyCars.Domain.Entities.User;
using BuyCars.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace BuyCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BussinesLogic _bl = new();


        [HttpGet("all")]
        [AdminMod]
        public IActionResult GetAll()
        {
            var userBl = _bl.GetUserBL();
            var users = userBl.GetAllUsers();
s
            var result = users.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.FirstName,
                u.LastName,
                u.Phone,
                u.Role,
                u.RegisteredOn,
                u.LastLogin
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [UserMod]
        public IActionResult GetById(int id)
        {
            var userBl = _bl.GetUserBL();
            var user = userBl.GetUserById(id);

            if (user == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.Role,
                user.RegisteredOn
            });
        }


        [HttpPut("{id:int}")]
        [UserMod]
        public IActionResult Update(int id, [FromBody] UserRegisterDto dto)
        {
            var currentUser = HttpContext.Items["CurrentUser"] as UserData;
            if (currentUser == null) return Unauthorized();

            if (currentUser.Id != id && currentUser.Role != "admin")
                return StatusCode(403, new { message = "Нет прав на редактирование этого профиля" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userBl = _bl.GetUserBL();
            var updated = userBl.UpdateUser(id, dto);

            if (updated == null)
                return NotFound(new { message = "Пользователь не найден" });

            return Ok(new
            {
                updated.Id,
                updated.UserName,
                updated.Email,
                updated.FirstName,
                updated.LastName,
                updated.Phone
            });
        }


        [HttpDelete("{id:int}")]
        [AdminMod]
        public IActionResult Delete(int id)
        {
            var userBl = _bl.GetUserBL();
            var deleted = userBl.DeleteUser(id);

            if (!deleted)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return NoContent();
        }
    }
}
