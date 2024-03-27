using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Sessions
{
    public sealed record class SessionCreated
    {
        public string AccountId { get; init; } = null!;

        public string Email = null!;

        public string Token = null!;

        public List<RoleKey> Roles = null!;

        public List<PermissionKey> Permissions = null!;
    }
}
