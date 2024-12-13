using CleanArchitecture.Authorization;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly IAccessControl<TRequest>[] accessControls;

    public AuthorizationDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        IEnumerable<IAccessControl<TRequest>>? accessControls)
    {
        this.next = next;
        this.accessControls = accessControls?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        if (!await accessControls.HasPermission(context.Actor, context.Request))
        {
            return ForbiddenError.Default;
        }

        return await next.Handle(context);
    }
}