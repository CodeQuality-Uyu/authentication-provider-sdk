namespace CQ.AuthProvider.SDK.Me;

public readonly struct UpdateMeArgs
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }
}