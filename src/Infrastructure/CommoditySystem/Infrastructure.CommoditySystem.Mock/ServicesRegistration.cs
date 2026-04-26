using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem.Mock;

public static class MockServicesRegistration
{
    public static void AddMockCommoditySystem(this IServiceCollection services)
    {
        ServicesConfiguration.RegisterMockServices(services);
    }

}
