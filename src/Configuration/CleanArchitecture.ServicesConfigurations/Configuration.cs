using CleanArchitecture.Configurations;
using CleanArchitecture.Ordering.Application;
using CleanArchitecture.ProcessManager;
using CleanArchitecture.Querying;
using CleanArchitecture.Shared;
using Infrastructure.RequestAudit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.ServicesConfigurations;

public static class Configuration
{
    public static void SetEnvironment(ApplicationFlavor application, IWebHostEnvironment environment)
    {
        SystemEnvironment.SetEnvironment(application, environment.DeploymentStage());
    }

    public static void SetEnvironment(ApplicationFlavor application, DeploymentStage stage)
    {
        SystemEnvironment.SetEnvironment(application, stage);
    }

    public static void ConfigureAppConfiguration(IConfigurationBuilder configuration, IEnvironment? environment = null)
    {
        environment ??= SystemEnvironment.Environment;

        configuration.Sources.Clear();
        Options.OptionsConfigs.Configure(configuration, environment);
        Secrets.SecretsConfigs.Configure(configuration, environment);
    }

    public static void AddAppSettings(IConfigurationBuilder configuration, IWebHostEnvironment environment)
    {
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnvironment? environment = null)
    {
        environment ??= SystemEnvironment.Environment;

        var connectionStrings = new ConnectionStrings(configuration.CleanArchitectureConnectionString());

        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton(_ => SystemEnvironment.Environment);

        services.AddDbInterceptors();
        services.AddMediator();
        services.AddActorAuthorization();

        services.AddCommoditySystem(environment);
        services.AddRequestAudit(connectionStrings.CleanArchitectureConnectionString);
        services.AddOrderingModule(connectionStrings.CleanArchitectureConnectionString);
        services.AddQueryingModule(connectionStrings.CleanArchitectureConnectionString);
        services.AddProcessManagerModule();

        services.AddScheduling();
        services.AddIntegrationEventProcessing();
        services.AddMessaging(configuration, connectionStrings, GlobalSettings.Messaging.MessagingSystem);
        services.AddDurableTasks(configuration, connectionStrings);
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