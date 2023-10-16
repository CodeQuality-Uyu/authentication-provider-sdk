using CQ.ServiceExtension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Config
{
    public static class AuthSdkDependencyInjection
    {
        public static void AddCqAuthService(this IServiceCollection services, string cqAuthApiUrl, LifeTime lifeTime)
        {
            services.AddService((serviceProvider) =>
            {
                return new HttpClientAdapter(cqAuthApiUrl);
            }, lifeTime);

            services.AddService<IAuthService, AuthService>(lifeTime);
        }
    }
}
