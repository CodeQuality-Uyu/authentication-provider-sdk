using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQ.AuthProvider.SDK.IntegrationTests
{
    public abstract class BaseIntegrationTest
    {
        private readonly IContainer Container;

        protected HealthService healthService => this.ResolveService<HealthService>();

        public BaseIntegrationTest() 
        {
            var webApiFactory = new IntegrationWebApiFactory();
            var baseAddress = webApiFactory.Server.BaseAddress;
            var client = webApiFactory.CreateClient();
            var task = client.GetAsync("health");
            task.Wait();

            var response = task.Result;

            var builder = new ContainerBuilder();

            builder.RegisterInstance(new AuthProviderApi($"{baseAddress.Scheme}://{baseAddress.Host}:{5000}")).SingleInstance();

            var knownSuffixes = new[] { "Service", };

            builder.RegisterAssemblyTypes(typeof(HealthService).Assembly)
                .Where(t => knownSuffixes.Any(s => t.Name.EndsWith(s)))
                .SingleInstance();


            Container = builder.Build();
        }

        private TService ResolveService<TService>() 
            where TService : class
        {
            return Container.Resolve<TService>();
        }
    }
}
