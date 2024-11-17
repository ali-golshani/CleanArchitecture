using CleanArchitecture.Ordering.Domain.Repositories;
using CleanArchitecture.Ordering.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Persistence;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IOrderingQueryDb, OrderingDbContext>();
        services.AddTransient<IOrderRepository, OrderRepository>();
    }
}
