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

    public ComplementaryProcess<TResponse> Follow<TComplementaryRequest>(
        TComplementaryRequest complementaryRequest,
        IRequestProcessor<TComplementaryRequest, TResponse> complementaryRequestProcessor)
    where TComplementaryRequest : Framework.Mediator.Requests.IRequest<TComplementaryRequest, TResponse>
    {
        var complementaryProcessFactory = delegate (Result<TResponse> result)
        {
            return new RequestProcess<TComplementaryRequest, TResponse>(complementaryRequest, complementaryRequestProcessor);
        };
        return new ComplementaryProcess<TResponse>(this, complementaryProcessFactory);
    }

    public ComplementaryProcess<TResponse> Follow<TComplementaryRequest>(
        Func<Result<TResponse>, TComplementaryRequest> complementaryRequestFactory,
        IRequestProcessor<TComplementaryRequest, TResponse> complementaryRequestProcessor)
    where TComplementaryRequest : Framework.Mediator.Requests.IRequest<TComplementaryRequest, TResponse>
    {
        var complementaryProcessFactory = delegate (Result<TResponse> result)
        {
            var complementaryRequest = complementaryRequestFactory(result);
            return new RequestProcess<TComplementaryRequest, TResponse>(complementaryRequest, complementaryRequestProcessor);
        };
        return new ComplementaryProcess<TResponse>(this, complementaryProcessFactory);
    }

    public CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest>(
        TCompensatorRequest compensatorRequest,
        IRequestProcessor<TCompensatorRequest, TResponse> compensatorRequestProcessor)
    where TCompensatorRequest : Framework.Mediator.Requests.IRequest<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            return new RequestProcess<TCompensatorRequest, TResponse>(compensatorRequest, compensatorRequestProcessor);
        };
        return new CompensatorProcess<TResponse>(this, compensatorProcessFactory);
    }

    public CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest>(
        Func<Result<TResponse>, TCompensatorRequest> compensatorRequestFactory,
        IRequestProcessor<TCompensatorRequest, TResponse> compensatorRequestProcessor)
    where TCompensatorRequest : Framework.Mediator.Requests.IRequest<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            var compensatorRequest = compensatorRequestFactory(result);
            return new RequestProcess<TCompensatorRequest, TResponse>(compensatorRequest, compensatorRequestProcessor);
        };
        return new CompensatorProcess<TResponse>(this, compensatorProcessFactory);
    }
}
