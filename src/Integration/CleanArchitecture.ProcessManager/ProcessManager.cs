using CleanArchitecture.ProcessManager.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

internal class ProcessManager(IServiceProvider serviceProvider) : IProcessManager
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return serviceProvider.SendToPipeline<TRequest, TResponse, RequestPipeline.Pipeline<TRequest, TResponse>>(request, cancellationToken);
    }
}