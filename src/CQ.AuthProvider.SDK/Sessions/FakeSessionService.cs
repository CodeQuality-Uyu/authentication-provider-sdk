using System.Security.Principal;
using CQ.AuthProvider.SDK.Accounts;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.SDK.Sessions;

internal sealed class FakeSessionService(IOptions<IPrincipal> accountLoggedOption) : ISessionService
{
    private readonly IPrincipal _accountLogged = accountLoggedOption.Value;

    public Task<SessionCreated> CreateAsync(CreateSessionArgs args)
    {
        return Task.FromResult(BuildFakeSession());
    }

    public Task<SessionCreated> CreateWithGoogleAsync(CreateSessionGoogleArgs args)
    {
        return Task.FromResult(BuildFakeSession());
    }

    private SessionCreated BuildFakeSession()
    {
        var fakeAccount = (AccountLogged)_accountLogged;

        return new SessionCreated
        {
            Id = fakeAccount.Id,
            ProfilePicture = fakeAccount.ProfilePicture,
            Email = fakeAccount.Email,
            FirstName = fakeAccount.FirstName,
            LastName = fakeAccount.LastName,
            FullName = fakeAccount.FullName,
            AppLogged = new SessionAppLogged
            {
                Id = fakeAccount.AppLogged.Id,
                Name = fakeAccount.AppLogged.Name,
            },
            Token = fakeAccount.Token,
            Permissions = fakeAccount.Permissions,
            Roles = fakeAccount.Roles,
        };
    }
}