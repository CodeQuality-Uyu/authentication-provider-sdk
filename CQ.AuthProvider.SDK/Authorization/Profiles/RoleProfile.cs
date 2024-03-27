using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization.Profiles
{
    internal sealed class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, CreateRoleRequest>()
                .ForMember(
                destination => destination.Key,
                options => options.MapFrom(
                    source => source.Key.ToString()))
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Permissions.Select(p => p.ToString())));

            CreateMap<List<Role>, CreateRoleBulkRequest>()
                .ForMember(
                destination => destination.Roles,
                options => options.MapFrom(
                    source => source));
        }
    }
}
