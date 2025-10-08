using CleanArchitecture.Configurations;
using CleanArchitecture.Shared;
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

    public static void ConfigureAppConfiguration(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.Sources.Clear();
        Options.OptionsConfigs.Configure(configuration, environment);
        Secrets.SecretsConfigs.Configure(configuration, environment);
    }

    public static void AddAppSettings(IConfigurationBuilder configuration, IWebHostEnvironment environment)
    {
        configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnvironment environment)
    {
        var connectionStrings = new ConnectionStrings(configuration.CleanArchitectureConnectionString());

        services.AddSingleton<IDateTime, SystemDateTime>();
        services.AddSingleton(_ => SystemEnvironment.Environment);

        Framework.Persistence.ServiceConfigurations.RegisterDbInterceptors(services);

        services.AddMediator();
        services.AddRequestAudit(connectionStrings);
        services.AddCommoditySystem(environment);
        services.AddMessaging(configuration, connectionStrings);
        services.AddOrderingModule(connectionStrings);
        services.AddQuerying(connectionStrings);
        services.AddProcessManager();
        services.AddAuthorization();
        services.AddScheduling();
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