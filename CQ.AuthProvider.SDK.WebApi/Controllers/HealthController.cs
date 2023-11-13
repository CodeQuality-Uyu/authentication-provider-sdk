using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.WebApi.Controllers
{
    [ApiController]
    [Route("/", Name = "Ping")]
    [Route("/health", Name = "Health Check")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { alive = true });
        }
    }
}
