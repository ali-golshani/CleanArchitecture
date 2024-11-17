using CleanArchitecture.Authorization;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class QueryFilteringDecorator<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
{
    private readonly IUseCase<TRequest, TResponse> next;
    private readonly IQueryFilter<TRequest>[] queryFilters;

    public QueryFilteringDecorator(
        IUseCase<TRequest, TResponse> next,
        IEnumerable<IQueryFilter<TRequest>> queryFilters)
    {
        this.next = next;
        this.queryFilters = queryFilters?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        var actor = context.Actor;
        var request = context.Request;

        foreach (var filter in queryFilters)
        {
            request = filter.Filter(actor, request);
        }

        context = new UseCaseContext<TRequest>
        {
            Actor = actor,
            Request = request,
            CancellationToken = context.CancellationToken
        };

        return await next.Handle(context);
    }
}