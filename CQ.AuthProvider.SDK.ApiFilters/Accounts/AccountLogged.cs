using System.Security;
using System.Security.Principal;

namespace CQ.AuthProvider.SDK.ApiFilters.Accounts;

public sealed record AccountLogged
    : IPrincipal
{
    public Guid Id { get; init; }

    public Multimedia ProfilePicture { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public string Token { get; init; } = null!;

    public List<string> Roles { get; init; } = null!;

    public List<string> Permissions { get; init; } = null!;

    public Tenant Tenant { get;init; } = null!;

    public IIdentity? Identity => null;

    public bool IsInRole(string role)
    {
        var isRole = Roles.Contains(role);

        return isRole || Permissions.Contains(role);
    }
}
