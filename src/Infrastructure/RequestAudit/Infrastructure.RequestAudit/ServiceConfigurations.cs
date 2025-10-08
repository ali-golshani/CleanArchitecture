using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RequestAudit;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<RequestAuditAgent>();
        services.AddTransient(typeof(RequestAuditMiddleware<,>));
    }

    public static void RegisterHostedServices(IServiceCollection services)
    {
        services.AddHostedService<RequestAuditBackgroundService>();
    }
}
