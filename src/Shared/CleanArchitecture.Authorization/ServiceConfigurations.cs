using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Authorization;

public static class ServiceConfigurations
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(AccessResolver<>));
    }
}
