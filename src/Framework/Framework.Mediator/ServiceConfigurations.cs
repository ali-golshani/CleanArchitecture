using Framework.Mediator.Notifications;
using Framework.Mediator.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(NotificationPublisher<>));
        services.AddScoped<IDomainEventBus, DomainEventBus>();
        services.AddTransient<INotificationPublisher, NotificationPublisher>();
    }
}
