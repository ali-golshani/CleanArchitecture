using Framework.Mediator.Extensions;
using Infrastructure.CommoditySystem.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddTransient(typeof(RequestPipeline<,>));
        services.AddTransient(typeof(ExceptionTranslationMiddleware<,>));
        services.AddScoped<ICommoditySystem, CommoditySystem>();
    }
}
