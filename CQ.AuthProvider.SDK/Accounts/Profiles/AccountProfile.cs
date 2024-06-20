using AutoMapper;
using CQ.AuthProvider.SDK.Abstractions.Accounts;

namespace CQ.AuthProvider.SDK.Accounts.Profiles;
internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<CreateAccountPassword, CreateAccountPasswordRequest>()
            .ForMember(
            destination => destination.Role,
            options => options.MapFrom(
                source => source.Role != null ? source.Role.ToString() : null));

        CreateMap<AccountCreatedResponse, Account>()
            .ForMember(
            destination => destination.Roles,
            options => options.MapFrom(
                source => source.Roles.Select(r => new RoleKey(r)).ToList()))

            .ForMember(
            destination => destination.Permissions,
            options => options.MapFrom(
                source => source.Permissions.Select(p => new PermissionKey(p))));

        CreateMap<AccountLoggedResponse, AccountCreated>()
            .IncludeBase<AccountCreatedResponse, Account>();
    }
}
