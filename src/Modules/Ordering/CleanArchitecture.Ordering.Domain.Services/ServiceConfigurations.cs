using CleanArchitecture.Ordering.Domain.Services.BuildOrder;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Domain.Services;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IBuildOrderService, BuildOrderService>();
        services.AddTransient<IOrderTrackingCodeBuilder, OrderTrackingCodeBuilder>();
        services.AddTransient<BuildOrderPolicyBuilder>();
    }
}
