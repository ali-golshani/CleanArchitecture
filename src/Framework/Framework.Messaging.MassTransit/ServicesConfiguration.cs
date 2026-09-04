using Framework.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Messaging.MassTransit;

public static class ServicesConfiguration
{
    public static void RegisterEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, MassTransitEventOutbox>();
    }
}
