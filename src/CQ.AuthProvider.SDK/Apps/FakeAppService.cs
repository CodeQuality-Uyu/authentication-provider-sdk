using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Apps;

internal sealed class FakeAppService : IAppService
{
    public Task<AppCreated> CreateClientAsync(CreateAppChildArgs args, AccountLogged accountLogged)
    {
        var fakeApp = new AppCreated
        {
            Id = Guid.NewGuid(),
        };

        return Task.FromResult(fakeApp);
    }

    public Task<AppDetailedInfo> GetByIdAsync(Guid id, AccountLogged accountLogged)
    {
        var fakeApp = new AppDetailedInfo
        {
            Id = id,
            Name = "Fake App",
            Logo = new Logo
            {
                Color = new BlobRead
                {
                    Url = "https://fake.com/logo-color.png",
                    Key = "logo-color.png"
                },
                Light = new BlobRead
                {
                    Url = "https://fake.com/logo-light.png",
                    Key = "logo-light.png"
                },
                Dark = new BlobRead
                {
                    Url = "https://fake.com/logo-dark.png",
                    Key = "logo-dark.png"
                }
            },
            Background = null
        };

        return Task.FromResult(fakeApp);
    }
}
