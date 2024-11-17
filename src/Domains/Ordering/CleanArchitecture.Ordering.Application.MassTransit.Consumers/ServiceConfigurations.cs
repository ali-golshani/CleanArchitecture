using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.MassTransit.Consumers;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterConsumer<OrderQueryConsumer>();
        services.RegisterConsumer<OrderStatusChangedEventConsumer>();
    }
}
