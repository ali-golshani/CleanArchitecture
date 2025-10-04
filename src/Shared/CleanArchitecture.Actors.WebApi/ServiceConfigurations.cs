using CleanArchitecture.Actors.ActorProviders;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors.WebApi;

public static class ServiceConfigurations
{
    public static void RegisterHttpActorsServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IActorProvider, HttpActorProvider>();
    }
}
