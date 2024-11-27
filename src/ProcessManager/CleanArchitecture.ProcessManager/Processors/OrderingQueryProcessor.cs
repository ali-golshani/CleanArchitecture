using CleanArchitecture.Ordering.Queries;
using Framework.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processors;

internal class OrderingQueryProcessor<TRequest, TResponse> : IRequestProcessor<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private readonly IQueryService queryService;

    public OrderingQueryProcessor(IQueryService queryService)
    {
        this.queryService = queryService;
    }

    public Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return queryService.Handle(request, cancellationToken);
    }
}
