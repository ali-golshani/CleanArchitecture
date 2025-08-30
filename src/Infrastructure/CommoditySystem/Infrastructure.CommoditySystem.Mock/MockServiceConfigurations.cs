using Framework.DependencyInjection.Extensions;
using Framework.Mediator.Extensions;
using Infrastructure.CommoditySystem.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class MockServiceConfigurations
{
    public static void RegisterMockServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddScoped<ICommoditySystem, CommoditySystem>();

        services.RegisterClosedImplementationsOf(typeof(IUseCase<,>), typeof(MockServiceConfigurations).Assembly);
    }
}
