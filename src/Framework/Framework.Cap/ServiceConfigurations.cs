using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cap;

public static class ServiceConfigurations
{
    public static void RegisterCapEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, CapEventOutbox>();
    }
}
