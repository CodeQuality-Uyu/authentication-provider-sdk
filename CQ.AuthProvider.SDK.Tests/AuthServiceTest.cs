using CQ.AuthProvider.SDK.Exceptions;
using CQ.Utility;
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
        public async Task GivenEmailInUse_WhenCreateAuth_ThenThrowException()
        {
            var httpClientMock = new Mock<HttpClientAdapter>();
            httpClientMock
                .Setup(c => c.PostAsync<Auth>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<Action<CqAuthErrorApi>>()))
                .Throws(new DuplicatedEmailException("some@email.com"));

            var authService = new AuthService(httpClientMock.Object);
            await authService.CreateAsync(new CreatePasswordAuth("some@email.com","some-password1!")).ConfigureAwait(false);
        }
    }
}