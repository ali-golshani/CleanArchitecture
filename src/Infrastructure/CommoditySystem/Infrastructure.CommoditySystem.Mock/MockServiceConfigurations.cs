using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class MockServiceConfigurations
{
    public static void RegisterMockServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddScoped<ICommoditySystem, CommoditySystem>();
    }
}
