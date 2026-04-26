using Framework.Persistence.Extensions;
using Framework.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application;

public static class ServicesRegistration
{
    public static void AddOrderingModule(this IServiceCollection services, string dbConnectionString)
    {
        services
            .AddDbContext<Persistence.OrderingDbContext>((sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(
                        dbConnectionString,
                        options =>
                        {
                            options.ConfigureMigrationsHistoryTable();
                        });
                optionsBuilder.AddInterceptors(sp.GetRequiredService<CorrelationIdInterceptor>());
            },
            ServiceLifetime.Scoped);

        Domain.Services.ServicesConfiguration.RegisterServices(services);
        Persistence.ServicesConfiguration.RegisterServices(services);
        Queries.ServicesConfiguration.RegisterServices(services);
        Commands.ServicesConfiguration.RegisterServices(services);
        ServicesConfiguration.RegisterServices(services);
    }

}
