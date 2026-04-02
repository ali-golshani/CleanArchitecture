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
                        options.MigrationsHistoryTable("DbMigrationsHistory", Persistence.Settings.SchemaNames.UserManagement);
                    });
                optionsBuilder.AddInterceptors(sp.GetRequiredService<CorrelationIdInterceptor>());
            },
            ServiceLifetime.Scoped);

        ServicesConfiguration.RegisterServices(services);
    }
}
