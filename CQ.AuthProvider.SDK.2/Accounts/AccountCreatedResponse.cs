namespace CQ.AuthProvider.SDK.Accounts;
internal record class AccountCreatedResponse
{
    public string Id { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public List<string> Roles { get; init; } = null!;

    public List<string> Permissions { get; init; } = null!;
}
