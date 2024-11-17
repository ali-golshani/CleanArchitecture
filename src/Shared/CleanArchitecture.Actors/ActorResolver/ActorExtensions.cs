using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors;

public static class ActorExtensions
{
    public static void ResolveActor(this IServiceProvider serviceProvider, Actor actor)
    {
        serviceProvider.GetRequiredService<FixedActorProvider>().SetUser(actor);
    }

    public static IServiceScope CreateScope(this IServiceScopeFactory serviceScopeFactory, Actor actor)
    {
        var scope = serviceScopeFactory.CreateScope();
        scope.ServiceProvider.ResolveActor(actor);
        return scope;
    }
}
