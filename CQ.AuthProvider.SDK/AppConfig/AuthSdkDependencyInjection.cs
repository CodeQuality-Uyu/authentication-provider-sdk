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

        public static IServiceCollection AddMigrationService(
            this IServiceCollection services,
            AuthProviderOptions authProviderOptions,
            LifeTime httpClientLifeTime = LifeTime.Scoped,
            LifeTime healthServiceLifeTime = LifeTime.Scoped,
            LifeTime permissionServiceLifeTime = LifeTime.Scoped,
            LifeTime roleServiceLifeTime = LifeTime.Scoped,
            LifeTime authProviderOptionsLifeTime = LifeTime.Scoped)
        {
            services
                .AddAutoMapper(
                typeof(RoleProfile),
                typeof(PermissionProfile))
                .AddService<AuthProviderApi>((serviceProvider) => new(authProviderOptions.Server), httpClientLifeTime)
                .AddService<IAuthHealthService, HealthService>(healthServiceLifeTime)
                .AddService<AuthProviderOptions>((privider) => authProviderOptions, authProviderOptionsLifeTime)
                .AddService<IRoleService, RoleService>(roleServiceLifeTime)
                .AddService<IPermissionService, PermissionService>(permissionServiceLifeTime);

            return services;
        }
    }
}
