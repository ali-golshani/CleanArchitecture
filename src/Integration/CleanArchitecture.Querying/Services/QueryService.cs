using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using CleanArchitecture.Mediator.Middlewares.Extensions;
using CleanArchitecture.Querying.Pipelines;
using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken, RequestExecutionOptions? options = null)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var correlationId = options?.CorrelationId
            ?? serviceProvider.GetRequiredService<ICorrelationIdAccessor>().CorrelationId
            ?? Guid.NewGuid();

        serviceProvider.UseCorrelationId(correlationId);

        var pipeline = serviceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(new RequestContext<TRequest>
        {
            Request = query.AsRequestType(),
            CancellationToken = cancellationToken,
            CorrelationId = correlationId,
            ExecutionStartTime = DateTime.Now,
        });
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken, RequestExecutionOptions? options = null)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var correlationId = options?.CorrelationId
            ?? serviceProvider.GetRequiredService<ICorrelationIdAccessor>().CorrelationId
            ?? Guid.NewGuid();

        serviceProvider.UseActor(actor);
        serviceProvider.UseCorrelationId(correlationId);

        var pipeline = serviceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(new RequestContext<TRequest>
        {
            Request = query.AsRequestType(),
            CancellationToken = cancellationToken,
            CorrelationId = correlationId,
            ExecutionStartTime = DateTime.Now,
        });
    }
}
