using Framework.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Messaging.Cap;

public static class ServicesConfiguration
{
    public static void RegisterCapEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, CapEventOutbox>();
    }
}
