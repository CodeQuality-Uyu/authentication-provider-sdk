namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class MeServiceTest
    {
        private readonly AuthProviderApi authProviderTestApi = new("http://localhost:7049");
        private readonly MeService meService;
        private readonly AuthService authService;
        private Auth auth;

        public MeServiceTest()
        {
            this.meService = new(this.authProviderTestApi);
            this.authService= new(this.authProviderTestApi);
        }

        [TestInitialize]
        public async Task OnInitAsync()
        {
            this.auth = await this.authService.CreateAsync(new CreateAuthPassword($"test@{Guid.NewGuid()}.com","!12345678", "admin")).ConfigureAwait(false);
        }


        [TestMethod]
        public async Task TestMethod1()
        {
            var authLogged = await this.meService.GetAsync(this.auth.Token).ConfigureAwait(false);

            Assert.AreEqual(auth.Email, authLogged.Email);
        }
    }
}