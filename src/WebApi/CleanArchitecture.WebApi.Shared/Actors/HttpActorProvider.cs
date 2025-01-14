﻿using CleanArchitecture.Actors;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Shared.Actors;

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

        actorResolved = true;
        var user = httpContextAccessor?.HttpContext?.User;
        actor = ResolveActor(user);
        return actor;
    }

    private static Actor? ResolveActor(ClaimsPrincipal? user)
    {
        if (user is null)
        {
            return null;
        }

        var actors = ClaimsActorResolver.Actors(user).ToList();

        if (actors.Count == 0)
        {
            return null;
        }

        return actors[0];
    }
}
