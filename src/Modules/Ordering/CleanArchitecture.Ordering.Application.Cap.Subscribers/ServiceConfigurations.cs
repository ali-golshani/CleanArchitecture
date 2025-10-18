using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Cap.Subscribers;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<OrderStatusChangedEventSubscriber>();
    }
}
