namespace CQ.AuthProvider.SDK.Accounts;

public readonly struct App()
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
}
