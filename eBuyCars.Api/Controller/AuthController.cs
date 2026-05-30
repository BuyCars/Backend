using eBuyCars.BusinessLogic;
using eBuyCars.Domain.Models.User;
using eBuyCars.Domains.Entities.User;
using Microsoft.AspNetCore.Mvc;

namespace eBuyCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BussinesLogic _bl = new();

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var session = _bl.GetSessionBL();

            var data = new ULoginData
            {
                Credential = loginDto.CredentialType,
                Password = loginDto.Password,
                LoginIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                LoginDateTime = DateTime.UtcNow
            };

            var result = session.UserLogin(data);

            if (!result.Status)
                return BadRequest(new { message = result.StatusMsg });

            return Ok(result);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userBl = _bl.GetUserBL();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var result = userBl.Register(registerDto, ip);

            if (!result.Status)
                return BadRequest(new { message = result.StatusMsg });

            return Created($"/api/user/{result.UserId}", result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["X-KEY"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token))
            {
                var session = _bl.GetSessionBL();
                session.UserLogout(token);
            }

            return Ok(new { message = "Выход выполнен" });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var token = Request.Headers["X-KEY"].FirstOrDefault();

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "X-KEY header отсутствует" });

            var session = _bl.GetSessionBL();
            var user = session.GetUserByCookie(token);

            if (user == null)
                return Unauthorized(new { message = "Недействительный или истёкший токен" });

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role,
                user.Phone
            });
        }
    }
}
