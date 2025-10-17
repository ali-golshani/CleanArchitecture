using CleanArchitecture.Actors.ActorProviders;
using CleanArchitecture.Actors.WebApi.ActorResolvers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi;

internal sealed class HttpActorProvider(IHttpContextAccessor httpContextAccessor) : IActorProvider
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    private bool actorResolved = false;
    private Actor? actor = null;

    public Actor? CurrentActor()
    {
        if (actorResolved)
        {
            return actor;
        }

        var user = httpContextAccessor?.HttpContext?.User;
        actor = ResolveActor(user);
        actorResolved = true;
        return actor;
    }

    private static Actor? ResolveActor(ClaimsPrincipal? user)
    {
        if (user is null)
        {
            return null;
        }

        var actors = Actors(user).ToList();

        if (actors.Count == 0)
        {
            return null;
        }

        return actors[0];
    }

    private static IEnumerable<Actor> Actors(ClaimsPrincipal principal)
    {
        var user = principal.GetUser();

        if (user is null)
        {
            yield break;
        }

        var resolvers = new IActorResolver<Actor>[]
        {
            new ProgrammerResolver(),
            new SupervisorActorResolver(),
            new BrokerActorResolver(),
            new CustomerActorResolver(),
        };

        foreach (var resolver in resolvers)
        {
            var result = resolver.Resolve(user);
            if (result is not null)
            {
                yield return result;
            }
        }
    }
}
