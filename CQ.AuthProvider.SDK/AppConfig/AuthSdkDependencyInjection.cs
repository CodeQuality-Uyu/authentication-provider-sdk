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
        public static IServiceCollection AddCqAuthService(
            this IServiceCollection services, 
            string cqAuthApiUrl, 
            LifeTime authServiceLifeTime = LifeTime.Transient, 
            LifeTime sessionServiceLifeTime = LifeTime.Transient)
        {
            services
                .AddTransient<HttpClientAdapter>((serviceProvider) =>
            {
                return new HttpClientAdapter(cqAuthApiUrl);
            })
                .AddService<IAuthService, AuthService>(authServiceLifeTime)
                .AddService<ISessionService, SessionService>(sessionServiceLifeTime);

            return services;
        }
    }
}
