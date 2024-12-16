using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(AuthorizationMiddleware<,>));
        services.AddTransient(typeof(ExceptionHandlingMiddleware<,>));
        services.AddTransient(typeof(FilteringMiddleware<,>));
        services.AddTransient(typeof(ValidationMiddleware<,>));
        services.AddTransient(typeof(RequestHandlingProcessor<,>));
    }
}
