﻿using Autofac;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.Abstractions.HealthCheck;
using CQ.AuthProvider.SDK.Accounts;
using CQ.AuthProvider.SDK.HealthChecks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CQ.AuthProvider.SDK.IntegrationTests;
public abstract class BaseIntegrationTest
{
    private readonly IContainer Container;

    protected IAuthHealthService HealthService => this.ResolveService<HealthService>();

    protected IAccountService AccountService => this.ResolveService<AccountService>();

    public BaseIntegrationTest()
    {
        var webApiFactory = new WebApplicationFactory<Program>();
        var client = webApiFactory.CreateClient();

        var builder = new ContainerBuilder();

        //builder.RegisterInstance(new AuthProviderApi(client)).SingleInstance();

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
