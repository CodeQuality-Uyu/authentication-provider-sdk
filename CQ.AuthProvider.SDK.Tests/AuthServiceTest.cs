using AutoMapper;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.AppConfig;
using CQ.Utility;
using Moq;

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

            var mapperMock = new Mock<IMapper>();

            var authService = new AccountService(
                httpClientMock.Object,
                authProviderOptionsMock.Object,
                mapperMock.Object);

            await authService.CreateForAsync(new CreateAccountPassword("some@email.com", "admin", "admin", "some-password1!", new RoleKey("some-role"))).ConfigureAwait(false);
        }
    }
}