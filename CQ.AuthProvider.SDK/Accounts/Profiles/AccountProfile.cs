using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Accounts.Profiles
{
    internal sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountPassword, CreateAccountPasswordRequest>()
                .ForMember(
                destination => destination.Role,
                options => options.MapFrom(
                    source => source.Role != null ? source.Role.ToString() : null));


            CreateMap<AccountResponse, Account>()
                .ForMember(
                destination => destination.Roles,
                options => options.MapFrom(
                    source => source.Roles.Select(r => new RoleKey(r)).ToList()))
                
                .ForMember(
                destination => destination.Permissions,
                options => options.MapFrom(
                    source => source.Permissions.Select(p => new PermissionKey(p))));

            CreateMap<AccountLoggedResponse, AccountCreated>()
                .IncludeBase<AccountResponse, Account>();
        }
    }
}
