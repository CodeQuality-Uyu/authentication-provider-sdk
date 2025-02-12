namespace CQ.AuthProvider.SDK.ApiFilters.Accounts;

public readonly struct App()
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
}
