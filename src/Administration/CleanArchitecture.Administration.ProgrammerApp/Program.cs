﻿using CleanArchitecture.ServicesConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Administration.ProgrammerApp;

internal static class Program
{
    public static async Task Main()
    {
        await CreateHostBuilder().Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                ;
    }

    private static void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder configuration)
    {
        Configuration.ConfigureAppConfiguration
        (
            configuration: configuration,
            optionsConfiguration: Configurations.GlobalSettings.OptionsConfiguration,
            secretsConfiguration: Configurations.GlobalSettings.SecretsConfiguration
        );
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        Configuration.RegisterServices(services, context.Configuration);
        services.AddScoped<MainApp>();

        services.AddHostedService<MainHostedService>();
    }

    private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
    {
        Configuration.ConfigureLogging(builder);
    }
}