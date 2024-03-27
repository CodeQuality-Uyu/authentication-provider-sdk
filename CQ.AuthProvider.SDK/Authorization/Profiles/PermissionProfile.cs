using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Authorization.Profiles
{
    internal sealed class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, CreatePermissionRequest>()
                .ForMember(
                destination => destination.Key,
                options => options.MapFrom(
                    source => source.Key.ToString()));

            CreateMap<List<Permission>, CreatePermissionBulkRequest>()
                .ForMember(destination => destination.Permissions,
                options => options.MapFrom(
                    source => source));

        }
    }
}
