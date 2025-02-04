using FluentValidation.AspNetCore;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class FluentValidationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = false;
        }).AddFluentValidationClientsideAdapters();
    }
}