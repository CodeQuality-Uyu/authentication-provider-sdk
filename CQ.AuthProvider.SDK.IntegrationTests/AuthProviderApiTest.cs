using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class AuthProviderApiTest
    {
        private readonly AuthProviderApi _authProviderTestApi = new("http://localhost:1000");

        [TestMethod]
        [ExpectedException(typeof(ConnectionRefusedException))]
        public async Task GivenAuthProviderShutDown_WhenGetUserLogged_ThenThrowException()
        {
            await this._authProviderTestApi.GetAsync<object>("some-resource").ConfigureAwait(false);
        }

    }
}
