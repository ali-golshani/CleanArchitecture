using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

internal class ProcessService(IServiceProvider serviceProvider) : IProcessManager
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return await serviceProvider.SendToHandler(command, cancellationToken);
    }
}