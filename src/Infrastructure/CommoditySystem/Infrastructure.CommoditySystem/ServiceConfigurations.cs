using Framework.Mediator.Extensions;
using Framework.Mediator.Middlewares;
using Infrastructure.CommoditySystem.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();

        services.AddTransient(typeof(RequestPipeline<,>));
        services.AddTransient(typeof(IPipeline<,>), typeof(RequestPipeline<,>));

        services.AddTransient(typeof(ExceptionTranslationMiddleware<,>));

        services.AddScoped<ICommoditySystem, CommoditySystem>();
    }
}
