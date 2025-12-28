namespace CQ.AuthProvider.SDK.Sessions;

internal sealed class FakeSessionService : ISessionService
{
    public Task<SessionCreated> CreateAsync(CreateSessionArgs args)
    {
        var fakeSession = new SessionCreated
        {
            Id = Guid.NewGuid(),
            ProfilePicture = null,
            Email = "email@fake.com",
            FirstName = "Fake",
            LastName = "User",
            FullName = "Fake User",
            AppLogged = new SessionAppLogged
            {
                Id = Guid.NewGuid(),
                Name = "Fake App",
            },
            Token = "fake-token",
            Permissions = [],
            Roles = []
        };

        return Task.FromResult(fakeSession);
    }
}