using AutoMapper;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.Accounts.Profiles;
using CQ.AuthProvider.SDK.Authorization;
using CQ.AuthProvider.SDK.Authorization.Profiles;
using CQ.AuthProvider.SDK.ClientSystems;
using CQ.AuthProvider.SDK.ClientSystems.Profiles;
using CQ.AuthProvider.SDK.HealthChecks;
using CQ.AuthProvider.SDK.Sessions;
using CQ.AuthProvider.SDK.Sessions.Profiles;
using CQ.ServiceExtension;
using Microsoft.Extensions.DependencyInjection;

namespace CQ.AuthProvider.SDK.AppConfig
{
    public static class AuthSdkDependencyInjection
    {
        /// <summary>
        /// Register the following necessary services:
        /// 1. AuthProviderApi
        /// 2. AuthService
        /// 3. SessionService
        /// 4. MeService
        /// 5. HealthService
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cqAuthApiUrl"></param>
        /// <param name="authServiceLifeTime"></param>
        /// <param name="sessionServiceLifeTime"></param>
        /// <returns></returns>
        public static IServiceCollection AddCqAuthService(
            this IServiceCollection services, 
            AuthProviderOptions authProviderOptions,
            LifeTime httpClientLifeTime = LifeTime.Scoped,
            LifeTime authServiceLifeTime = LifeTime.Scoped, 
            LifeTime sessionServiceLifeTime = LifeTime.Scoped,
            LifeTime clientSystemLifeTime = LifeTime.Scoped,
            LifeTime healthServiceLifeTime = LifeTime.Scoped,
            LifeTime authProviderOptionsLifeTime = LifeTime.Scoped)
        {
            services
                .AddAutoMapper(
                typeof(AccountProfile),
                typeof(RoleProfile),
                typeof(PermissionProfile),
                typeof(SessionProfile),
                typeof(ClientSystemProfile))
                .AddService<AuthProviderApi>((serviceProvider) => new(authProviderOptions.Server), httpClientLifeTime)
                .AddService<IAccountService, AccountService>(authServiceLifeTime)
                .AddService<IClientSystemsService, ClientSystemService>(clientSystemLifeTime)
                .AddService<ISessionService, SessionService>(sessionServiceLifeTime)
                .AddService<IAuthHealthService, HealthService>(healthServiceLifeTime)
                .AddService<AuthProviderOptions>((privider) => authProviderOptions, authProviderOptionsLifeTime);

            return services;
        }

        public static async Task<IServiceCollection> AddRolesSeedDataAsync(
            this IServiceCollection services,
            List<Role> roles)
        {
            var authProviderApi = (AuthProviderApi)services
                .Where(s => s.ImplementationType == typeof(AuthProviderApi))
                .First()
                .ImplementationInstance!;

            var authProviderOptions = (AuthProviderOptions)services
                .Where(s => s.ImplementationType == typeof(AuthProviderOptions))
                .First().ImplementationInstance!;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleProfile>();
            });

            var mapper = new Mapper(config);

            var roleService = new RoleService(
                authProviderApi,
                authProviderOptions,
                mapper);

            await roleService.AddBulkAsync(roles).ConfigureAwait(false);

            return services;
        }

        public static async Task<IServiceCollection> AddPermissionsSeedDataAsync(
            this IServiceCollection services,
            List<Permission> permissions)
        {
            var authProviderApi = (AuthProviderApi)services
                .Where(s => s.ImplementationType == typeof(AuthProviderApi))
                .First()
                .ImplementationInstance!;

            var authProviderOptions = (AuthProviderOptions)services
                .Where(s => s.ImplementationType == typeof(AuthProviderOptions))
                .First()
                .ImplementationInstance!;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PermissionProfile>();
            });

            var mapper = new Mapper(config);

            var roleService = new PermissionService(
                authProviderApi,
                authProviderOptions,
                mapper);

            await roleService.AddBulkAsync(permissions).ConfigureAwait(false);

            return services;
        }
    }
}
