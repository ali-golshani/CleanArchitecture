using FluentValidation.AspNetCore;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class FluentValidationConfigs
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = false;
        }).AddFluentValidationClientsideAdapters();
    }
}