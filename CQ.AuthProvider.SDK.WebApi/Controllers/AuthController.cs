using Microsoft.AspNetCore.Mvc;

namespace CQ.AuthProvider.SDK.WebApi.Controllers
{
    [ApiController]
    [Route("auths")]
    public class AuthController : ControllerBase
    {
        [HttpPost("credentials")]
        public dynamic Create(CreateAuthCredentialRequest request)
        {
            return new
            {
                email= request.Email,
                roles= new List<string> { request.Role }
            };
        }
    }

    public sealed record class CreateAuthCredentialRequest(string Email, string Password, string Role);
}
