using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MassTransit;

public static class ServiceConfigurations
{
    public static void RegisterEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IDomainEventOutbox, EventOutbox>();
    }
}
