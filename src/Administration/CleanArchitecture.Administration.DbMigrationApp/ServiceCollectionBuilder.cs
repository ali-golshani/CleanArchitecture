using CleanArchitecture.Configurations;
using CleanArchitecture.ServicesConfigurations;
using MassTransit;
using MassTransit.SqlTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

public static class ServiceCollectionBuilder
{
    private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

    public static IServiceCollection Build(out IConfiguration configuration)
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(AppPath);
        Configuration.SetEnvironment(ApplicationFlavor.DbMigration, DeploymentStage.Development);
        Configuration.ConfigureAppConfiguration(configurationBuilder, SystemEnvironment.Environment);
        configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        Configuration.ConfigureServices(services, configuration, SystemEnvironment.Environment);
        services.AddLogging(Configuration.ConfigureLogging);
        ConfigureMassTransitMigrationServices(services);

        return services;
    }

    private static void ConfigureMassTransitMigrationServices(IServiceCollection services)
    {
        services.AddSqlServerMigrationHostedService(create: true, delete: false);
        services.AddTransient<SqlTransportMigrationHostedService>();
    }
}
