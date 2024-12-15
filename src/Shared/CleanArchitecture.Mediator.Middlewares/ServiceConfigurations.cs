using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(AuthorizationFilter<,>));
        services.AddTransient(typeof(ExceptionHandlingFilter<,>));
        services.AddTransient(typeof(TransformingFilter<,>));
        services.AddTransient(typeof(ValidationFilter<,>));
    }
}
