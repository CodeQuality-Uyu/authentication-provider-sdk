using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
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
                .Setup(c => c.PostAsync<AccountResponse>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    It.IsAny<List<Header>>()))
                .Throws(new CqAuthException(CqAuthErrorCode.DuplicatedEmail, "Duplicated email"));

            var authProviderOptionsMock = new Mock<AuthProviderOptions>();

            var authService = new AccountService(httpClientMock.Object, authProviderOptionsMock.Object);

            await authService.CreateForAsync(new CreateAccountPassword("some@email.com", "admin", "admin", "some-password1!", new RoleKey("some-role"))).ConfigureAwait(false);
        }
    }
}