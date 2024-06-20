using AutoMapper;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.Abstractions.Sessions;
using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.Sessions.Profiles;
internal sealed class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<SessionResponse, SessionCreated>()
            .ForMember(
            destination => destination.Roles,
            options => options.MapFrom(
                source => source.Roles.Select(r => new RoleKey(r)).ToList()))
            .ForMember(
            destination => destination.Permissions,
            options => options.MapFrom(
                source => source.Permissions.Select(r => new PermissionKey(r)).ToList()));
    }
}
