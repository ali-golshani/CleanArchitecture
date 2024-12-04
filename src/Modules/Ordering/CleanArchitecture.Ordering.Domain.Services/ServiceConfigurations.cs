using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Domain.Services;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IRegisterOrderService, RegisterOrderService>();
        services.AddTransient<IOrderTrackingCodeBuilder, OrderTrackingCodeBuilder>();
        services.AddTransient<CustomerCommodityRule>();
    }
}
