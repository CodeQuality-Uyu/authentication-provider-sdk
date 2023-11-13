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
        [ExpectedException(typeof(CqAuthException))]
        public async Task GivenEmailInUse_WhenCreateAuth_ThenThrowException()
        {
            var httpClientMock = new Mock<AuthProviderApi>();
            httpClientMock
                .Setup(c => c.PostAsync<Auth>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<IList<Header>>()))
                .Throws(new CqAuthException(CqAuthErrorCode.DuplicatedEmail,"Duplicated email"));

            var authService = new AuthService(httpClientMock.Object);

            await authService.CreateAsync(new CreateAuthPassword("some@email.com","some-password1!","some-role")).ConfigureAwait(false);
        }
    }
}