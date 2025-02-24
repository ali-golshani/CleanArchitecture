using Framework.Mediator.DomainEvents;
using Framework.Mediator.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddTransient(typeof(DomainEventPublisher<>));
        services.AddScoped<IIntegrationEventBus, IntegrationEventBus>();
        services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
    }
}
