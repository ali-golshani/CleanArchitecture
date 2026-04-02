using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MassTransit;

internal static class Configs
{
    public static void RegisterMassTransitOutbox(IServiceCollection services)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddEntityFrameworkOutbox<MassTransitDbContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });
        });
    }

    public static void RegisterMassTransitOutboxAndTransport(IServiceCollection services, string connectionString)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddSqlMessageScheduler();

            cfg.AddEntityFrameworkOutbox<MassTransitDbContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
            });

            cfg.UsingSqlServer((context, x) =>
            {
                x.UseSqlMessageScheduler();
                x.ConfigureEndpoints(context);
                x.AutoStart = true;
            });
        });

        services.AddOptions<SqlTransportOptions>().Configure(options =>
        {
            options.ConnectionString = connectionString;
        });
    }
}