using CleanArchitecture.Actors;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class QueryService(ActorPreservingScopeFactory scopeFactory) : IQueryService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope();
        return await scope.ServiceProvider.SendByMediator(query, cancellationToken);
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope(actor);
        return await scope.ServiceProvider.SendByMediator(query, cancellationToken);
    }
}
