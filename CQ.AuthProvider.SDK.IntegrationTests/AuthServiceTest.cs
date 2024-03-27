using CQ.AuthProvider.SDK.Accounts;
using CQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class AuthServiceTest : BaseIntegrationTest
    {

        #region Create
        [TestMethod]
        public async Task WhenCredentialsValid_ThenReturnNewAuth()
        {
            var auth = await base.AccountService.CreateForAsync(new("email@gmail.com", "admin", "admin", "!12345678", new RoleKey("role"))).ConfigureAwait(false);

            Assert.IsNotNull(auth);
            Assert.AreEqual("email@gmail.com", auth.Email);
            CollectionAssert.Contains(auth.Roles.ToList(), new RoleKey("role"));
        }
        #endregion
    }
}
