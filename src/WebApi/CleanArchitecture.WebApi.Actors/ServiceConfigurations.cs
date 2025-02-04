using CleanArchitecture.Actors;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Actors;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IActorProvider, HttpActorProvider>();
    }
}
