using Framework.Application.Requests;
using Framework.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<RequestContextAccessor>();
        services.AddScoped<IRequestContextAccessor>(sp => sp.GetRequiredService<RequestContextAccessor>());
    }
}
