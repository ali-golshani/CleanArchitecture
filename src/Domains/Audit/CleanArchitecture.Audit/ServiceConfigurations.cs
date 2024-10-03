using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Audit;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<CommandAuditAgent>();
        services.AddSingleton<QueryAuditAgent>();
        services.AddSingleton<ICommandAuditAgent>(x => x.GetRequiredService<CommandAuditAgent>());
        services.AddSingleton<IQueryAuditAgent>(x => x.GetRequiredService<QueryAuditAgent>());
    }
}
