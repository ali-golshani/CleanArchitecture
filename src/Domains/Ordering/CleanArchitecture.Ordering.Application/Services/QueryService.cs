using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.UseCases;
using Framework.Exceptions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var useCase = serviceProvider.GetRequiredService<QueryUseCase<TRequest, TResponse>>();
        if (query is not TRequest request) throw new ProgrammerException();
        return useCase.Handle(request, cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var useCase = serviceProvider.GetRequiredService<QueryUseCase<TRequest, TResponse>>();
        if (query is not TRequest request) throw new ProgrammerException();
        return useCase.Handle(actor, request, cancellationToken);
    }
}
