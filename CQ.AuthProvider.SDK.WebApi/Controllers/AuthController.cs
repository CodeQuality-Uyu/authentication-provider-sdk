using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.WebApi.Controllers
{
    [ApiController]
    [Route("auths")]
    public class AuthController : ControllerBase
    {
        [HttpPost("credentials")]
        public IActionResult Create(CreateAuthCredentialRequest request)
        {
            return Ok(request);
        }
    }

    public sealed record class CreateAuthCredentialRequest(string Email);
}
