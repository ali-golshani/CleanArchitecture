using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class MockServicesConfiguration
{
    public static void RegisterMockServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddScoped<ICommoditySystem, MockCommoditySystem>();
    }
}
