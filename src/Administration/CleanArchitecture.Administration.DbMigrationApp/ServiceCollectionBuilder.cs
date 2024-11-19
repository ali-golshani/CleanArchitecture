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
        var services = new ServiceCollection();

        configurationBuilder.SetBasePath(AppPath);
        SystemEnvironment.SetDbMigrationEnvironment();
        Configuration.ConfigureAppConfiguration(configurationBuilder, SystemEnvironment.Environment);

        configuration = configurationBuilder.Build();

        Configuration.ConfigureServices(services, configuration);
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
