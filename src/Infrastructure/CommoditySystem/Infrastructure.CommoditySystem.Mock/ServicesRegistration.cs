using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class MockServicesRegistration
{
    public static void AddMockCommoditySystem(this IServiceCollection services)
    {
        MockServicesConfiguration.RegisterMockServices(services);
    }

}
