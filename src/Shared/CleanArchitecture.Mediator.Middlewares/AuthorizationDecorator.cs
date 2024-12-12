using CleanArchitecture.Authorization;
using Framework.Results.Errors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly IAccessVerifier<TRequest>[] accessVerifiers;

    public AuthorizationDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers)
    {
        this.next = next;
        this.accessVerifiers = accessVerifiers?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        if (!await accessVerifiers.IsAccessible(context.Actor, context.Request))
        {
            return AccessDeniedError.Default;
        }

        return await next.Handle(context);
    }
}