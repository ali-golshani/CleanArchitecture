using CleanArchitecture.Ordering.Queries;
using Framework.ProcessManager;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Extensions;

internal class OrderingQueryProcess<TRequest, TResponse> : IProcess<TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private readonly IQueryService queryService;
    private readonly TRequest request;

    public OrderingQueryProcess(IQueryService queryService, TRequest request)
    {
        this.queryService = queryService;
        this.request = request;
    }

    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        return queryService.Handle(request, cancellationToken);
    }
}
