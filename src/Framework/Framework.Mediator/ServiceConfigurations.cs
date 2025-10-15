using Framework.Mediator.Notifications;
using Framework.Mediator.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(NotificationPublisher<>));
        services.AddScoped<IIntegrationEventBus, IntegrationEventBus>();
        services.AddTransient<INotificationPublisher, NotificationPublisher>();
    }
}
