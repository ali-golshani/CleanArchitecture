using CleanArchitecture.Configurations;
using CleanArchitecture.ServicesConfigurations.Configs;
using Framework.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations;

internal static class ServicesRegistration
{
    public static void AddOrderingModule(this IServiceCollection services, ConnectionStrings connectionStrings)
    {
        services
            .AddDbContext<Ordering.Persistence.OrderingDbContext>((sp, optionsBuilder) =>
            {
                SqlConfigs.Configure(optionsBuilder, connectionStrings.CleanArchitectureConnectionString, Ordering.Persistence.Settings.SchemaNames.Ordering);
                optionsBuilder.AddInterceptors(sp.GetRequiredService<CorrelationIdInterceptor>());
            },
            ServiceLifetime.Scoped);

        Ordering.Domain.Services.ServiceConfigurations.RegisterServices(services);
        Ordering.Persistence.ServiceConfigurations.RegisterServices(services);
        Ordering.Queries.ServiceConfigurations.RegisterServices(services);
        Ordering.Commands.ServiceConfigurations.RegisterServices(services);
        Ordering.Application.ServiceConfigurations.RegisterServices(services);
        Ordering.Application.Cap.Subscribers.ServiceConfigurations.RegisterServices(services);
        Ordering.Application.MassTransit.Consumers.ServiceConfigurations.RegisterServices(services);
    }

    public static void AddCommoditySystem(this IServiceCollection services, IEnvironment environment)
    {
        if (environment.DeploymentStage == DeploymentStage.Staging)
        {
            Infrastructure.CommoditySystem.MockServiceConfigurations.RegisterMockServices(services);
        }
        else
        {
            Infrastructure.CommoditySystem.ServiceConfigurations.RegisterServices(services);
        }
    }

    public static void AddRequestAudit(this IServiceCollection services, ConnectionStrings connectionStrings)
    {
        services
            .AddDbContext<Infrastructure.RequestAudit.Persistence.AuditDbContext>(optionsBuilder =>
            SqlConfigs.Configure(optionsBuilder, connectionStrings.CleanArchitectureConnectionString, Infrastructure.RequestAudit.Settings.Persistence.SchemaNames.Audit),
            ServiceLifetime.Scoped);

        Infrastructure.RequestAudit.ServiceConfigurations.RegisterServices(services);
        Infrastructure.RequestAudit.ServiceConfigurations.RegisterHostedServices(services);
    }

    public static void AddProcessManager(this IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
    {
        ProcessManager.ServiceConfigurations.RegisterServices(services);
    }

    public static void AddQuerying(this IServiceCollection services, ConnectionStrings connectionStrings)
    {
        services
            .AddDbContext<Querying.Persistence.EmptyDbContext>(
            optionsBuilder => optionsBuilder.UseSqlServer(connectionStrings.CleanArchitectureConnectionString));

        Querying.ServiceConfigurations.RegisterServices(services);
    }

    public static void AddMediator(this IServiceCollection services)
    {
        Framework.Mediator.ServiceConfigurations.RegisterServices(services);
        Mediator.Middlewares.ServiceConfigurations.RegisterServices(services);
    }

    public static void AddScheduling(this IServiceCollection services)
    {
        Framework.Scheduling.ServiceConfigurations.RegisterServices(services);
        Framework.Scheduling.ServiceConfigurations.RegisterHostedServices(services);
        Scheduling.ServiceConfigurations.RegisterJobs(services);
    }

    public static void AddDbInterceptors(this IServiceCollection services)
    {
        Framework.Persistence.ServiceConfigurations.RegisterDbInterceptors(services);
    }

    public static void AddActorAuthorization(this IServiceCollection services)
    {
        Actors.ServiceConfigurations.RegisterServices(services);
        Authorization.ServiceConfigurations.RegisterServices(services);
    }

    public static void AddMessaging(this IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
    {
        if (GlobalSettings.Messaging.MessagingSystem == MessagingSystem.Cap)
        {
            services.AddCapMessaging(configuration, connectionStrings);
        }

        if (GlobalSettings.Messaging.MessagingSystem == MessagingSystem.MassTransit)
        {
            services.AddMassTransitMessaging(connectionStrings);
        }
    }

    private static void AddCapMessaging(this IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
    {
        CapConfigs.RegisterCap(services, configuration, connectionStrings.CleanArchitectureConnectionString);
        Framework.Cap.ServiceConfigurations.RegisterCapEventOutbox(services);
    }

    private static void AddMassTransitMessaging(this IServiceCollection services, ConnectionStrings connectionStrings)
    {
        services
            .AddDbContext<Framework.MassTransit.MassTransitDbContext>(
            optionsBuilder => SqlConfigs.Configure(optionsBuilder, connectionStrings.CleanArchitectureConnectionString, Framework.MassTransit.Settings.Persistence.SchemaNames.MassTransit),
            ServiceLifetime.Scoped);

        MassTransitConfigs.RegisterMassTransitOutboxAndTransport(services, connectionStrings.CleanArchitectureConnectionString);
        services.AddHostedService<Framework.MassTransit.BusHostedService>();

        Framework.MassTransit.ServiceConfigurations.RegisterEventOutbox(services);
    }
}