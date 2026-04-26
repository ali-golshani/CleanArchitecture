using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem.Mock;

public static class ServicesConfiguration
{
    public static void RegisterMockServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddScoped<ICommoditySystem, CommoditySystem>();
    }
}
