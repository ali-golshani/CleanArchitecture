using Framework.Persistence.Interceptors;
using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class DbContextConfigs
{
    public static void RegisterDbContexts(this IServiceCollection services, string connectionString)
    {
        services
            .AddDbContext<Ordering.Persistence.OrderingDbContext>((sp, optionsBuilder) =>
            {
                SqlConfigs.Configure(optionsBuilder, connectionString, Ordering.Persistence.Settings.SchemaNames.Ordering);
                optionsBuilder.AddInterceptors(sp.GetRequiredService<CorrelationIdInterceptor>());
            },
            ServiceLifetime.Scoped);

        services
            .AddDbContext<AuditDbContext>(optionsBuilder =>
            SqlConfigs.Configure(optionsBuilder, connectionString, Infrastructure.RequestAudit.Settings.Persistence.SchemaNames.Audit),
            ServiceLifetime.Scoped);

        services
            .AddDbContext<Framework.MassTransit.MassTransitDbContext>(
            optionsBuilder => SqlConfigs.Configure(optionsBuilder, connectionString, Framework.MassTransit.Settings.Persistence.SchemaNames.MassTransit),
            ServiceLifetime.Scoped);

        services
            .AddDbContext<Querying.Persistence.EmptyDbContext>(
            optionsBuilder => optionsBuilder.UseSqlServer(connectionString));
    }
}
