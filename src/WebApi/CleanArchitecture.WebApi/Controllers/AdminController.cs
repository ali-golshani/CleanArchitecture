﻿using CleanArchitecture.Actors;
using CleanArchitecture.WebApi.Authentication;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers;

public class AdminController : BaseController
{
    [HttpGet("get-actor")]
    public Results<Ok<Actor>, NotFound> GetActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    [HttpGet("authorize-default-actor")]
    [Authorize(AuthenticationSchemes.SchemeAPolicy)]
    public Results<Ok<Actor>, NotFound> GetImeActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    [HttpGet("authorize-services-actor")]
    [Authorize(AuthenticationSchemes.SchemeBPolicy)]
    public Results<Ok<Actor>, NotFound> GetOnlineActor(IActorResolver actorResolver)
    {
        return Get(actorResolver);
    }

    private static Results<Ok<Actor>, NotFound> Get(IActorResolver actorResolver)
    {
        return actorResolver.Actor.AsOkOrNotFound();
    }
}
