﻿using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<QueryPipeline<TRequest, TResponse>>();
        return pipeline.Handle(query.AsRequestType(), cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        serviceProvider.ResolveActor(actor);
        return Handle(query, cancellationToken);
    }
}
