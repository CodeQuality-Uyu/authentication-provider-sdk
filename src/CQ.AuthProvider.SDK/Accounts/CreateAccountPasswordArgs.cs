namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct CreateAccountPasswordArgs()
{
    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Password { get; init; }

    public required string Locale { get; init; }

    public required string TimeZone { get; init; }

    public bool IsPasswordHashed { get; init; }

    public Guid AppId { get; init; }
}
