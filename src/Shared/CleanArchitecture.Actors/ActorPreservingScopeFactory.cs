using CleanArchitecture.Actors.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Actors;

public sealed class ActorPreservingScopeFactory(IServiceProvider serviceProvider) : IServiceScopeFactory
{
    public IServiceScope CreateScope()
    {
        var actorResolver = serviceProvider.GetRequiredService<IActorResolver>();
        return CreateScope(actorResolver.Actor);
    }

    public IServiceScope CreateScope(Actor? actor)
    {
        var scope = serviceProvider.CreateScope();

        if (actor is not null)
        {
            scope.ServiceProvider.ResolveActor(actor);
        }

        return scope;
    }
}
