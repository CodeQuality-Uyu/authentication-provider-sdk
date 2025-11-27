namespace CQ.AuthProvider.SDK.Apps;

public readonly struct CreateAppChildArgs()
{
    public string Name { get; init; } = null!;
    public Logo Logo { get; init; } = null!;
    public Background? Background { get; init; } = null!;
    public Owner Owner { get; init; }
}

public readonly struct Owner()
{
    public string Firstname { get; init; } = null!;
    public string Lastname { get; init; } = null!;
    public string Email { get; init; } = null!;
    public IList<Guid> RoleIds { get; init; } = [];
}
