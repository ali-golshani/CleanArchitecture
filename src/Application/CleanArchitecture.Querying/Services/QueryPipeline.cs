﻿using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryPipeline<TRequest, TResponse> :
    RequestPipeline<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipeline(
        IActorResolver actorResolver,
        QueryPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver, pipelineBuilder)
    {
    }
}