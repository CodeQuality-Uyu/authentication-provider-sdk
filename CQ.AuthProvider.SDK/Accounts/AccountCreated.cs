namespace CQ.AuthProvider.SDK.Accounts
{
    public sealed record class AccountCreated
    {
        public readonly string Id;

        public readonly string FullName;

        public readonly string FirstName;

        public readonly string LastName;

        public readonly string Email;

        public readonly string Token;

        public readonly List<RoleKey> Roles;

        public readonly List<PermissionKey> Permissions;

        public AccountCreated(
            string id,
            string fullName,
            string firstName,
            string lastName,
            string email,
            string token,
            List<string> roles,
            List<string> permissions)
        {
            Id = id;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Token = token;
            Roles = roles.Select(r => new RoleKey(r)).ToList();
            Permissions = permissions.Select(p => new PermissionKey(p)).ToList();
        }
    }
}
