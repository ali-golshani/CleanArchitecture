using Framework.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Persistence;

public static class ServicesConfiguration
{
    public static void RegisterDbInterceptors(IServiceCollection services)
    {
        services.AddScoped<CorrelationIdInterceptor>();
    }
}
