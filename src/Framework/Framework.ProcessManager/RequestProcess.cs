using Framework.Mediator.Requests;
using Framework.Results;

namespace Framework.ProcessManager;

internal sealed class RequestProcess<TRequest, TResponse> : IProcess<TResponse>
    where TRequest : IRequest<TRequest, TResponse>
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
