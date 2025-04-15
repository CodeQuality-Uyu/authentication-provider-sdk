namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct CreateAccountPasswordArgs()
{
    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Password { get; init; }

    public Guid? RoleId { get; init; }
}
