using CleanArchitecture.Authorization;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationFilter<TRequest, TResponse> :
    IFilter<TRequest, TResponse>
{
    private readonly IAccessControl<TRequest>[] accessControls;

    public AuthorizationFilter(IEnumerable<IAccessControl<TRequest>>? accessControls)
    {
        this.accessControls = accessControls?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IPipe<TRequest, TResponse> pipe)
    {
        if (await accessControls.IsAccessDenied(context.Actor, context.Request))
        {
            return ForbiddenError.Default;
        }

        return await pipe.Send(context);
    }
}