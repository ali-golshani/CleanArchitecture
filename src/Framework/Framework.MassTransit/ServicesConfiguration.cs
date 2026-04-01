using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MassTransit;

public static class ServicesConfiguration
{
    public static void RegisterEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, MassTransitEventOutbox>();
    }
}
