using CleanArchitecture.Actors.ActorProviders;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors;

public static class ServiceConfigurations
{
    public static void RegisterActorsServices(IServiceCollection services)
    {
        services.AddScoped<FixedActorProvider>();
        services.AddScoped<IActorProvider, FixedActorProvider>(sp => sp.GetRequiredService<FixedActorProvider>());
        services.AddScoped<IActorResolver, ActorResolver>();

        services.AddScoped<ActorPreservingScopeFactory>();
    }
}
