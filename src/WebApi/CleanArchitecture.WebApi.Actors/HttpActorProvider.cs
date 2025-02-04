using CleanArchitecture.Actors;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Actors;

internal class HttpActorProvider : IActorProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    private bool actorResolved = false;
    private Actor? actor = null;

    public HttpActorProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

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

        var actors = ClaimsPrincipalExtensions.Actors(user).ToList();

        if (actors.Count == 0)
        {
            return null;
        }

        return actors[0];
    }
}
