using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class ServicesRegistration
{
    public static void AddCommoditySystem(this IServiceCollection services)
    {
        ServicesConfiguration.RegisterServices(services);
    }

}
