namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct CreateAccountForArgs()
{
    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public string Locale { get; init; } = "en-UY";

    public string TimeZone { get; init; } = "America/Montevideo";

    public string? ProfilePictureKey { get; init; }

    public required IList<Guid> AppIds { get; init; }

    public required IList<Guid> RoleIds { get; init; }
}

