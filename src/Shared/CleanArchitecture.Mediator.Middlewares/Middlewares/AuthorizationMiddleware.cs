using CleanArchitecture.Authorization;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    private readonly IAccessControl<TRequest>[] accessControls;

    public AuthorizationMiddleware(IEnumerable<IAccessControl<TRequest>>? accessControls)
    {
        this.accessControls = accessControls?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        if (await accessControls.IsAccessDenied(context.Actor, context.Request))
        {
            return ForbiddenError.Default;
        }

        return await next.Handle(context);
    }
}