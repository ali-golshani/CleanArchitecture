using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Audit;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<CommandAuditAgent>();
        services.AddSingleton<QueryAuditAgent>();
    }
}
