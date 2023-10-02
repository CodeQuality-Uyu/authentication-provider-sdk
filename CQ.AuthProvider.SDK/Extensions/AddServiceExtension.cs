using CQ.AuthProvider.SDK.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.Extensions
{
    internal static class AddServiceExtension
    {
        public static void AddService<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, LifeTime lifeTime)
            where TService : class
            where TImplementation : class, TService
        {
            switch (lifeTime)
            {
                case LifeTime.Scoped:
                    {
                        services.AddScoped<TService>(implementationFactory);
                        break;
                    }
                case LifeTime.Transient:
                    {
                        services.AddTransient<TService>(implementationFactory);
                        break;
                    }
                case LifeTime.Singleton:
                    {
                        services.AddSingleton<TService>(implementationFactory);
                        break;
                    }
            }
        }
    }
}
