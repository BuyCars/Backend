using Microsoft.AspNetCore.Mvc;

namespace eBuyCars.Api.Controller
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
