using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors;

public static class ActorExtensions
{
    public static void ResolveActor(this IServiceProvider serviceProvider, Actor actor)
    {
        serviceProvider.GetRequiredService<FixedActorProvider>().SetActor(actor);
    }
}
