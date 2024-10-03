using Framework.Exceptions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var useCase = serviceProvider.GetRequiredService<QueryUseCase<TRequest, TResponse>>();
        if (command is not TRequest request) throw new ProgrammerException();
        return useCase.Execute(request, cancellationToken);
    }
}
