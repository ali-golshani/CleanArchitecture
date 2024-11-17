using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.BackgroundServices;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddHostedService<CommandAuditService>();
        services.AddHostedService<QueryAuditService>();
    }
}
