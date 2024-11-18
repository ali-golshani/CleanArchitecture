using CleanArchitecture.Actors;
using CleanArchitecture.WebApi.Authentication;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

public class AdminController : BaseController
{
    [HttpGet("actor")]
    public Results<Ok<Actor>, NotFound> GetActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    [HttpGet("default-actor")]
    [Authorize(AuthenticationSchemes.DefaultPolicy)]
    public Results<Ok<Actor>, NotFound> GetImeActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    [HttpGet("services-actor")]
    [Authorize(AuthenticationSchemes.InternalServicesPolicy)]
    public Results<Ok<Actor>, NotFound> GetOnlineActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    private static Results<Ok<Actor>, NotFound> Get(IActorResolver actorResolver)
    {
        return actorResolver.Actor.ToOkOrNotFound();
    }
}
