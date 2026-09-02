using Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.SqlEventBus;

public static class ServicesConfiguration
{
    public static void RegisterSqlEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, SqlEventOutbox>();
    }
}
