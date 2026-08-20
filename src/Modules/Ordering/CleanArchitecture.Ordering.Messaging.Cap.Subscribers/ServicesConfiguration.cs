using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Messaging.Cap.Subscribers;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<OrderStatusChangedEventSubscriber>();
    }
}
