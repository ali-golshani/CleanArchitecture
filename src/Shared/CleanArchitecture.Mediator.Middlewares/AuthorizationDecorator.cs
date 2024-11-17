using CleanArchitecture.Authorization;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class AuthorizationDecorator<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
{
    private readonly IUseCase<TRequest, TResponse> next;
    private readonly IAccessVerifier<TRequest>[] accessVerifiers;

    public AuthorizationDecorator(
        IUseCase<TRequest, TResponse> next,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers)
    {
        this.next = next;
        this.accessVerifiers = accessVerifiers?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        if (!await accessVerifiers.IsAccessible(context.Actor, context.Request))
        {
            return AccessDeniedError.Default;
        }

        return await next.Handle(context);
    }
}