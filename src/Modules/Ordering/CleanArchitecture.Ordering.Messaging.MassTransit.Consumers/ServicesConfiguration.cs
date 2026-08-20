using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Messaging.MassTransit.Consumers;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterConsumer<GetOrderQueryConsumer>();
        services.RegisterConsumer<OrderStatusChangedEventConsumer>();
    }
}
