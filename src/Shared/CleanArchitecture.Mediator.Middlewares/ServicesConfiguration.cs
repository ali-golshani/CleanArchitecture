using Framework.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Framework.Mediator;

namespace CleanArchitecture.Mediator.Middlewares;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(AuthorizationMiddleware<,>));
        services.AddTransient(typeof(ExceptionHandlingMiddleware<,>));
        services.AddTransient(typeof(FilteringMiddleware<,>));
        services.AddTransient(typeof(ValidationMiddleware<,>));
        services.AddScoped<CorrelationIdAccessor>();
        services.AddScoped<ICorrelationIdAccessor>(sp => sp.GetRequiredService<CorrelationIdAccessor>());
        services.AddScoped<ICorrelationIdProvider>(sp => sp.GetRequiredService<CorrelationIdAccessor>());
        services.AddScoped<RequestExecutionScopeFactory>();
    }
}
