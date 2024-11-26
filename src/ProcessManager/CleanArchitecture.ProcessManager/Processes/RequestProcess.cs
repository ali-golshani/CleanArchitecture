//using CleanArchitecture.Ordering.Commands;
//using Framework.Mediator.Requests;
//using Framework.Results;

//namespace CleanArchitecture.ProcessManager.Processes;

//internal class RequestProcess<TRequest, TResponse> : IProcess<TResponse>
//    where TRequest : Framework.Mediator.Requests.IRequest<TRequest, TResponse>
//{
//    private readonly TRequest request;
//    private readonly IRequestProcessor<TRequest, TResponse> processor;

//    public RequestProcess(TRequest request, IRequestProcessor<TRequest, TResponse> processor)
//    {
//        this.request = request;
//        this.processor = processor;
//    }

//    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
//    {
//        return processor.Handle(request, cancellationToken);
//    }

//    public ComplementaryProcess<TResponse> Follow<TComplementaryRequest>(
//        Func<Result<TResponse>, TComplementaryRequest> complementaryRequestFactory)
//    where TComplementaryRequest : CommandBase, ICommand<TComplementaryRequest, TResponse>
//    {
//        var complementaryProcessFactory = delegate (Result<TResponse> result)
//        {
//            var complementaryRequest = complementaryRequestFactory(result);
//            return new OrderingCommandProcess<TComplementaryRequest, TResponse>(complementaryRequest, commandService);
//        };
//        return new ComplementaryProcess<TResponse>(this, complementaryProcessFactory);
//    }

//    public CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest>(
//        Func<Result<TResponse>, TCompensatorRequest> compensatorRequestFactory)
//    where TCompensatorRequest : CommandBase, ICommand<TCompensatorRequest, TResponse>
//    {
//        var compensatorProcessFactory = delegate (Result<TResponse> result)
//        {
//            var compensatorRequest = compensatorRequestFactory(result);
//            return new OrderingCommandProcess<TCompensatorRequest, TResponse>(compensatorRequest, commandService);
//        };
//        return new CompensatorProcess<TResponse>(this, compensatorProcessFactory);
//    }
//}
