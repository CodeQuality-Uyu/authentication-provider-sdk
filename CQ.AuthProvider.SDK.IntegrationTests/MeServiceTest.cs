using CQ.Utility;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class MeServiceTest
    {
        private readonly AuthProviderApi authProviderTestApi = new("https://localhost:7049");
        private readonly MeService meService;
        private readonly AuthService authService;
        private Auth auth;

        public MeServiceTest()
        {
            this.meService = new(this.authProviderTestApi);
            this.authService = new(this.authProviderTestApi);
        }

        [TestInitialize]
        public async Task OnInitAsync()
        {
            this.auth = await this.authService.CreateAsync(new CreateAuthPassword($"test@{Guid.NewGuid()}.com", "!12345678", "admin")).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(CqAuthException))]
        public async Task GivenEmptyToken_WhenGetUserLoggd_ThenThrowException()
        {
            var authLogged = await this.meService.GetAsync("any-auth-token").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GivenAuthToken_WhenGetUserLoggd_ThenReturnAuth()
        {
            var authLogged = await this.meService.GetAsync(this.auth.Token).ConfigureAwait(false);

            Assert.AreEqual(auth.Email, authLogged.Email);
        }
    }
}