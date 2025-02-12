using System.Security.Principal;

namespace CQ.AuthProvider.SDK.ApiFilters.Accounts;

public record AccountLogged
    : IPrincipal
{
    public Guid Id { get; init; }

    public BlobRead? ProfilePicture { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Locale { get; init; } = null!;

    public string TimeZone { get; init; } = null!;

    public string Token { get; init; } = null!;

    public List<string> Roles { get; init; } = [];

    public List<string> Permissions { get; init; } = [];

    public App AppLogged { get;init; }

    public Tenant Tenant { get; init; }

    public IIdentity? Identity => null;

    public bool IsInRole(string role)
    {
        var isRole = Roles.Contains(role);

        return isRole || Permissions.Contains(role);
    }
}
