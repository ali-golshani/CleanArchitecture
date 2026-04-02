using CleanArchitecture.Configurations;
using CleanArchitecture.ProcessManager;
using CleanArchitecture.ServicesConfigurations.OptionsProviders;
using Framework.Cap;
using Framework.DurableTask;
using Framework.MassTransit;
using Infrastructure.CommoditySystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations;

internal static class ServicesRegistration
{
    public static void AddDbInterceptors(this IServiceCollection services)
    {
        Framework.Persistence.ServicesConfiguration.RegisterDbInterceptors(services);
    }

    public static void AddMediator(this IServiceCollection services)
    {
        Framework.Mediator.ServicesConfiguration.RegisterServices(services);
        Mediator.Middlewares.ServicesConfiguration.RegisterServices(services);
    }

    public static void AddActorAuthorization(this IServiceCollection services)
    {
        Actors.ServicesConfiguration.RegisterServices(services);
        Authorization.ServicesConfiguration.RegisterServices(services);
    }

    public static void AddCommoditySystem(this IServiceCollection services, IEnvironment environment)
    {
        if (environment.DeploymentStage == DeploymentStage.Staging)
        {
            services.AddMockCommoditySystem();
        }
        else
        {
            services.AddCommoditySystem();
        }
    }

    public static void AddScheduling(this IServiceCollection services)
    {
        Framework.Scheduling.ServicesConfiguration.RegisterServices(services);
        Framework.Scheduling.ServicesConfiguration.RegisterHostedServices(services);
        Scheduling.ServicesConfiguration.RegisterJobs(services);
    }

    public static void AddIntegrationEventProcessing(this IServiceCollection services)
    {
        Ordering.Application.Cap.Subscribers.ServicesConfiguration.RegisterServices(services);
        Ordering.Application.MassTransit.Consumers.ServicesConfiguration.RegisterServices(services);
    }

    public static void AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        ConnectionStrings connectionStrings,
        MessagingSystem messagingSystem)
    {
        if (messagingSystem == MessagingSystem.Cap)
        {
            var capOptions = configuration.CapOptions();
            services.AddCapMessaging(capOptions, connectionStrings.CleanArchitectureConnectionString);
        }

        if (messagingSystem == MessagingSystem.MassTransit)
        {
            services.AddMassTransitMessaging(connectionStrings.CleanArchitectureConnectionString);
        }
    }

    public static void AddDurableTasks(
        this IServiceCollection services,
        IConfiguration configuration,
        ConnectionStrings connectionStrings,
        string taskHubname)
    {
        var durableTaskOptions = configuration.DurableTaskOptions();
        var orchestrationsRegistrar = new DurableTaskOrchestrationsRegistrar();

        services.RegisterDurableTask
        (
            taskHubname: taskHubname,
            dbConnectionString: connectionStrings.CleanArchitectureConnectionString,
            durableTaskOptions: durableTaskOptions,
            orchestrationsRegistrar: orchestrationsRegistrar
        );
    }
}