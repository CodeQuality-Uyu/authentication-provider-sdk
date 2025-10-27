namespace CQ.AuthProvider.SDK.Abstractions.Accounts;
public record class Account
{
    public string Id { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public List<RoleKey> Roles { get; init; } = null!;

    public List<PermissionKey> Permissions { get; init; } = null!;

    public bool HasPermission(PermissionKey permission)
    {
        return Permissions.Contains(permission);
    }
}
