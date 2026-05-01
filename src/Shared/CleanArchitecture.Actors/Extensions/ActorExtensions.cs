using CleanArchitecture.Actors.ActorProviders;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors.Extensions;

internal static class ActorExtensions
{
    public static void UseActor(this IServiceProvider serviceProvider, Actor actor)
    {
        serviceProvider.GetRequiredService<FixedActorProvider>().SetActor(actor);
    }
}
