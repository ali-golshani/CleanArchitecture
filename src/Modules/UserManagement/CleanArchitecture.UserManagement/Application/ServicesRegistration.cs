using Framework.Persistence.Extensions;
using Framework.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UserManagement.Application;

public static class ServicesRegistration
{
    public static void AddUserManagementModule(this IServiceCollection services, string dbConnectionString)
    {
        services
            .AddDbContext<Persistence.UserManagementDbContext>((sp, optionsBuilder) =>
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

        ServicesConfiguration.RegisterServices(services);
    }
}
