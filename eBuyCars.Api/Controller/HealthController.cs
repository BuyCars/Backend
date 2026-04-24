using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyCars.Api.Controller
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult Ping()
        {
            return Ok("Healthy");
        }

    }
}
