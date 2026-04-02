using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MassTransit;

public static class ServicesRegistration
{
    public static void AddMassTransitMessaging(this IServiceCollection services, string dbConnectionString)
    {
        services
            .AddDbContext<MassTransitDbContext>(
            optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                    dbConnectionString,
                    options =>
                    {
                        options.MigrationsHistoryTable("DbMigrationsHistory", Settings.Persistence.SchemaNames.MassTransit);
                    });
            },
            ServiceLifetime.Scoped);

        Configs.RegisterMassTransitOutboxAndTransport(services, dbConnectionString);
        services.AddHostedService<BusHostedService>();

        ServicesConfiguration.RegisterEventOutbox(services);
    }
}
