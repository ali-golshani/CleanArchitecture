using Framework.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RequestAudit;

public static class ServicesRegistration
{
    public static void AddRequestAudit(this IServiceCollection services, string dbConnectionString)
    {
        services
            .AddDbContext<Persistence.AuditDbContext>((sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(
                    dbConnectionString,
                    options =>
                    {
                        options.MigrationsHistoryTable("DbMigrationsHistory", Settings.Persistence.SchemaNames.Audit);
                    });
                optionsBuilder.AddInterceptors(sp.GetRequiredService<CorrelationIdInterceptor>());
            },
            ServiceLifetime.Scoped);

        ServicesConfiguration.RegisterServices(services);
        ServicesConfiguration.RegisterHostedServices(services);
    }
}
