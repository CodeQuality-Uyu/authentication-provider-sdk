using CQ.AuthProvider.SDK.Accounts;
using CQ.Utility;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    [TestClass]
    public class MeServiceTest : BaseIntegrationTest
    {
        #region GetAuthLogged

        [TestMethod]
        [ExpectedException(typeof(CqAuthException))]
        public async Task WhenAuthTokenInvalid_ThenThrowException()
        {
            await this.meService.GetByTokenAsync("any-auth-token").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task WhenUserLogged_ThenReturnAuth()
        {
            var token = Guid.NewGuid().ToString();

            var authLogged = await this.meService.GetByTokenAsync(token).ConfigureAwait(false);

            Assert.AreEqual("some@gmail.com",authLogged.Email);
            CollectionAssert.Contains(authLogged.Roles.ToList(), new RoleKey("role"));
        }
        #endregion

        #region HasPermission
        [TestMethod]
        [ExpectedException(typeof(CqAuthException))]
        public async Task WhenInvalidToken_ThenThrowException()
        {
            await this.meService.HasPermissionAsync(new PermissionKey("valid-permission"), "invalid-auth-token").ConfigureAwait(false);
        }

        [TestMethod]
        public async Task WhenValidPermission_ThenReturnTrue()
        {
            var response = await this.meService.HasPermissionAsync(new PermissionKey("valid-permission"), Guid.NewGuid().ToString()).ConfigureAwait(false);

            Assert.IsTrue(response);
        }

        [TestMethod]
        public async Task WhenInValidPermission_ThenReturnFalse()
        {
            var response = await this.meService.HasPermissionAsync(new PermissionKey("another-permission"), Guid.NewGuid().ToString()).ConfigureAwait(false);

            Assert.IsFalse(response);
        }
        #endregion
    }
}