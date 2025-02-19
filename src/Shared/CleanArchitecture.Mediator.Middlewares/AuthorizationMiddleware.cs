using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using Framework.Mediator.Middlewares;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    private readonly IActorResolver actorResolver;
    private readonly IAccessControl<TRequest>[] accessControls;

    public AuthorizationMiddleware(
        IActorResolver actorResolver,
        IEnumerable<IAccessControl<TRequest>>? accessControls)
    {
        this.actorResolver = actorResolver;
        this.accessControls = accessControls?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var actor = actorResolver.Actor;

        if (actor is null)
        {
            return UnauthorizedError.Default;
        }

        if (await accessControls.IsAccessDenied(actor, context.Request))
        {
            return ForbiddenError.Default;
        }

        return await next.Handle(context);
    }
}