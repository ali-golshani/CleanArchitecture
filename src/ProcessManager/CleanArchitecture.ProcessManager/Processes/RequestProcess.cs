using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal class RequestProcess<TRequest, TResponse> : IProcess<TResponse>
    where TRequest : Framework.Mediator.Requests.IRequest<TRequest, TResponse>
{
    private readonly TRequest request;
    private readonly IRequestProcessor<TRequest, TResponse> processor;

    public RequestProcess(TRequest request, IRequestProcessor<TRequest, TResponse> processor)
    {
        this.request = request;
        this.processor = processor;
    }

    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        return processor.Handle(request, cancellationToken);
    }
}
