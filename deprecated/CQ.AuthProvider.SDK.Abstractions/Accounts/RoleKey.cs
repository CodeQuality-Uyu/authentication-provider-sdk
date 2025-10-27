using CQ.Utility;

namespace CQ.AuthProvider.SDK.Abstractions.Accounts;
public record class RoleKey
{
    private readonly string Value;

    public RoleKey(string value)
    {
        Value = Guard.Encode(value, nameof(value));
    }

    public override string ToString()
    {
        return Value;
    }
}
