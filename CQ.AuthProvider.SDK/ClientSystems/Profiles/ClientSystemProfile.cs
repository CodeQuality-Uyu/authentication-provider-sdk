using AutoMapper;
using CQ.AuthProvider.SDK.Accounts;

namespace CQ.AuthProvider.SDK.ClientSystems.Profiles
{
    internal sealed class ClientSystemProfile : Profile
    {
        public ClientSystemProfile()
        {
            CreateMap<ClientSystemResponse, ClientSystem>()
                .ForMember(destination => destination.Role,
                options => options.MapFrom(
                    source => new RoleKey(source.Role)))
                
                .ForMember(destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Permissions.Select(p => new PermissionKey(p)).ToList()));
        }
    }
}
