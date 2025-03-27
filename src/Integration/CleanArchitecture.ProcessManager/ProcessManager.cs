using CleanArchitecture.ProcessManager.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager;

internal class ProcessManager(IServiceProvider serviceProvider) : IProcessManager
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<RequestPipeline<TRequest, TResponse>>();
        return pipeline.Handle(request.AsRequestType(), cancellationToken);
    }
}