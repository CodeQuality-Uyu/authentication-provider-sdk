using CQ.AuthProvider.SDK.Accounts;
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
            string cqAuthApiUrl, 
            LifeTime httpClientLifeTime = LifeTime.Scoped,
            LifeTime authServiceLifeTime = LifeTime.Scoped, 
            LifeTime sessionServiceLifeTime = LifeTime.Scoped,
            LifeTime meServiceLifeTime = LifeTime.Scoped,
            LifeTime healthServiceLifeTime = LifeTime.Scoped)
        {
            services
                .AddService<AuthProviderApi>((serviceProvider) => new (cqAuthApiUrl), httpClientLifeTime)
                .AddService<IAccountService, AccountService>(authServiceLifeTime)
                .AddService<ISessionService, SessionService>(sessionServiceLifeTime)
                .AddService<IMeService, MeService>(meServiceLifeTime)
                .AddService<IAuthHealthService, HealthService>(healthServiceLifeTime);

            return services;
        }
    }
}
