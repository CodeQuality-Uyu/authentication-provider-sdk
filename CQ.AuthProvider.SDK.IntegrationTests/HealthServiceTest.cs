using CQ.AuthProvider.SDK.HealthChecks;

namespace CQ.AuthProvider.SDK.IntegrationTests;
[TestClass]
public class HealthServiceTest : BaseIntegrationTest
{
    //[TestMethod]
    //public async Task IsActive_WhenApiIsShutDown_ThenReturnFalse()
    //{
    //    var localHealthService = new HealthService(new AuthProviderApi("http://localhost:1000"));

    //    var response = await localHealthService.IsActiveAsync().ConfigureAwait(false);

    //    Assert.IsFalse(response);
    //}

    //[TestMethod]
    //public async Task IsActive_WhenApiIsUp_ThenReturnTrue()
    //{
    //    var response = await base.HealthService.IsActiveAsync().ConfigureAwait(false);

    //    Assert.IsTrue(response);
    //}
}
