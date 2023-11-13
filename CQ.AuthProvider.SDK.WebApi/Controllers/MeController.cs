using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CQ.AuthProvider.SDK.WebApi.Controllers
{
    [ApiController]
    [Route("me")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Create([FromHeader] string authorization)
        {
            var isGuid = Guid.TryParse(authorization, out var id);

            if (isGuid)
            {
                return Ok(new { token = authorization });
            }

            return BadRequest();
        }

        [HttpPost("check-permission")]
        public IActionResult CheckPermission([FromHeader] string authorization, [FromBody] CheckPermissionRequest request)
        {
            var response = Create(authorization);

            if(response.GetType() != typeof(OkObjectResult))
            {
                return response;
            }

            return Ok(new { hasPermission = request.Permission == "valid-permission" });
        }
    }

    public sealed record class CheckPermissionRequest(string Permission);
}
