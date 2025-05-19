using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using Framework.Mediator.Middlewares;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
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

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
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