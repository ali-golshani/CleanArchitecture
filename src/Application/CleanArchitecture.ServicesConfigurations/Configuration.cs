using CleanArchitecture.Configurations;
using CleanArchitecture.ServicesConfigurations.Configs;
using CleanArchitecture.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.ServicesConfigurations;

public static class Configuration
{
    public static void ConfigureAppConfiguration(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.Sources.Clear();
        GlobalConfigs.RegisterSettings(configuration);
        configuration.AddJsonStream(Options.Options.ConfigurationStream(environment.OptionsConfiguration));
        configuration.AddEnvironmentVariables(Secrets.EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.Authentication.ConfigurationStream(environment.SecretsConfiguration));
        configuration.AddJsonStream(Secrets.ConnectionStrings.ConfigurationStream(environment.SecretsConfiguration));
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.CleanArchitectureConnectionString();

        DbContextConfigs.RegisterDbContexts(services, connectionString);

        CapConfigs.RegisterCap(services, connectionString);
        MassTransitConfigs.RegisterMassTransitOutboxAndTransport(services, connectionString);

        if (GlobalSettings.Messaging.MessagingSystem == MessagingSystem.Cap)
        {
            Framework.Cap.ServiceConfigurations.RegisterServices(services);
            Ordering.Application.Cap.Subscribers.ServiceConfigurations.RegisterServices(services);
        }

        if (GlobalSettings.Messaging.MessagingSystem == MessagingSystem.MassTransit)
        {
            Framework.MassTransit.ServiceConfigurations.RegisterServices(services);
            Ordering.Application.MassTransit.Consumers.ServiceConfigurations.RegisterServices(services);

            services.AddHostedService<Framework.MassTransit.BusHostedService>();
        }

        Framework.Scheduling.ServiceConfigurations.RegisterServices(services);
        Framework.Scheduling.ServiceConfigurations.RegisterHostedServices(services);
        Framework.Mediator.ServiceConfigurations.RegisterServices(services);

        Actors.ServiceConfigurations.RegisterServices(services);

        Audit.ServiceConfigurations.RegisterServices(services);

        Ordering.Domain.Services.ServiceConfigurations.RegisterServices(services);
        Ordering.Persistence.ServiceConfigurations.RegisterServices(services);
        Ordering.Queries.Handlers.ServiceConfigurations.RegisterServices(services);
        Ordering.Commands.Handlers.ServiceConfigurations.RegisterServices(services);
        Ordering.Application.ServiceConfigurations.RegisterServices(services);

        Infrastructure.CommoditySystem.ServiceConfigurations.RegisterServices(services);

        ProcessManager.ServiceConfigurations.RegisterServices(services);
        Querying.ServiceConfigurations.RegisterServices(services);
        Scheduling.ServiceConfigurations.RegisterServices(services);
        BackgroundServices.ServiceConfigurations.RegisterServices(services);

        services.AddSingleton<IDateTime, SystemDateTime>();
    }

    public static void ConfigureLogging(ILoggingBuilder builder)
    {
        builder
            .ClearProviders()
            .AddSeq()
            .SetMinimumLevel(LogLevel.Error)
            ;
    }
}