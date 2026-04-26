using CleanArchitecture.Actors.ActorProviders;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors.Extensions;

public static class ActorExtensions
{
    public static void ResolveActor(this IServiceProvider serviceProvider, Actor actor)
    {
        serviceProvider.GetRequiredService<FixedActorProvider>().SetActor(actor);
    }
}
