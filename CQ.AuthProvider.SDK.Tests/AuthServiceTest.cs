using CQ.AuthProvider.SDK.Exceptions;
using Moq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text.Json;

namespace CQ.AuthProvider.SDK.Tests
{
    [TestClass]
    public class AuthServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(DuplicatedEmailException))]
        public async void GivenEmailInUse_WhenCreateAuth_ThenThrowException()
        {
            var authUrl = "auth-provider-url";
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock
                .Setup(c => c.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<JsonSerializerOptions>(), CancellationToken.None))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent<CqErrorApi>(
                       new CqErrorApi
                       {
                           Code = "DuplicatedEmail",
                           Message = "Email is in use"
                       },
                       new JsonMediaTypeFormatter())
                });

            var authService = new AuthService(authUrl);
            await authService.LoginAsync("some-email", "some-password");
        }
    }
}