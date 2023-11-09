using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class HealthServiceTest
    {
        private readonly AuthProviderApi _authProviderTestApi = new("http://localhost:7049");
        private readonly HealthService _healthService;

        public HealthServiceTest()
        {
            this._healthService = new HealthService(this._authProviderTestApi);
        }

        [TestMethod]
        public async Task GivenAuthProviderShutDown_WhenGetHealth_ThenFalse()
        {
            var response = await this._healthService.IsActiveAsync().ConfigureAwait(false);

            Assert.IsFalse(response);
        }
    }
}
