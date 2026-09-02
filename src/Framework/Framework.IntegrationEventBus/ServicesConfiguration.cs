using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.IntegrationEventBus;

public static class ServicesConfiguration
{
    public static void RegisterCapEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, EventOutbox>();
    }
}
