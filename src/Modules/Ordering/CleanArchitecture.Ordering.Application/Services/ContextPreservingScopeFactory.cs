using CleanArchitecture.Actors;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class ContextPreservingScopeFactory(IServiceProvider serviceProvider) : IServiceScopeFactory
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
