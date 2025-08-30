using Framework.DependencyInjection.Extensions;
using Framework.Mediator.Extensions;
using Infrastructure.CommoditySystem.Pipelines;
using Infrastructure.CommoditySystem.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddTransient(typeof(RequestPipeline<,>));
        services.AddTransient(typeof(ExceptionTranslationMiddleware<,>));
        services.AddScoped<ICommoditySystem, CommoditySystem>();

        services.RegisterClosedImplementationsOf(typeof(IUseCase<,>), typeof(ServiceConfigurations).Assembly);
    }
}
