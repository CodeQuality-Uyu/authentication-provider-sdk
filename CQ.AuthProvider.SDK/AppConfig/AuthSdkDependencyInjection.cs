using CQ.ServiceExtension;
using CQ.Utility;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.AppConfig
{
    public static class AuthSdkDependencyInjection
    {
        /// <summary>
        /// Register the following necessary services:
        /// 1. HttpClientAdapter -> Transient
        /// 2. AuthService
        /// 3. SessionService
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
            LifeTime sessionServiceLifeTime = LifeTime.Scoped)
        {
            services
                .AddService<AuthProviderApi>((serviceProvider) =>
            {
                return new (cqAuthApiUrl);
            }, httpClientLifeTime)
                .AddService<IAuthService, AuthService>(authServiceLifeTime)
                .AddService<ISessionService, SessionService>(sessionServiceLifeTime);

            return services;
        }
    }
}
