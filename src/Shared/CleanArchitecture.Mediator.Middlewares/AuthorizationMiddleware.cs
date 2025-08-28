using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using Framework.Results.Errors;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
{
    private readonly IActorResolver actorResolver;
    private readonly AccessResolver<TRequest> accessResolver;

    public AuthorizationMiddleware(
        IActorResolver actorResolver,
        AccessResolver<TRequest> accessResolver)
    {
        this.actorResolver = actorResolver;
        this.accessResolver = accessResolver;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        var actor = actorResolver.Actor;

        if (actor is null)
        {
            return UnauthorizedError.Default;
        }

        var accessLevel = await accessResolver.AccessLevel(actor, context.Request);

        if (accessLevel == AccessLevel.Denied)
        {
            return ForbiddenError.Default;
        }

        return await next.Handle(context);
    }
}