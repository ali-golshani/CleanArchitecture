using Framework.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Persistence;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<CorrelationIdInterceptor>();
    }
}
