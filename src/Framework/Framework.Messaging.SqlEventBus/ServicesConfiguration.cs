using Framework.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Messaging.SqlEventBus;

public static class ServicesConfiguration
{
    public static void RegisterSqlEventOutbox(IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventOutbox, SqlEventOutbox>();
    }
}
