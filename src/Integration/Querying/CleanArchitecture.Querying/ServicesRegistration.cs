using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying;

public static class ServicesRegistration
{
    public static void AddQueryingModule(this IServiceCollection services, string dbConnectionString)
    {
        services
            .AddDbContext<Persistence.EmptyDbContext>(
            optionsBuilder => optionsBuilder.UseSqlServer(dbConnectionString));

        ServicesConfiguration.RegisterServices(services);
    }
}
