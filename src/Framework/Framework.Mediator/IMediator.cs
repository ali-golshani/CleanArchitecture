﻿namespace Framework.Mediator;

public interface IMediator
{
    Task<Result<TResponse>> Send<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>;
}
