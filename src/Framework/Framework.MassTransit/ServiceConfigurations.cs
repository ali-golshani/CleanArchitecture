using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MassTransit;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, EventOutbox>();
    }
}
