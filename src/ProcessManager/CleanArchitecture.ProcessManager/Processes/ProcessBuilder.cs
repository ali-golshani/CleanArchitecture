using CleanArchitecture.Ordering.Commands;
using Framework.Exceptions;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal static class ProcessBuilder
{
    public static IRequestProcessor<TRequest, TResponse> Processor<TRequest, TResponse>(
        this ICommandService commandService)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return new OrderingCommandProcessor<TRequest, TResponse>(commandService);
    }

    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this ICommandService commandService,
        Framework.Mediator.Requests.IRequest<TRequest, TResponse> request)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return new RequestProcess<TRequest, TResponse>
        (
            request as TRequest ?? throw new ProgrammerException(),
            commandService.Processor<TRequest, TResponse>()
        );
    }

    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this IRequestProcessor<TRequest, TResponse> processor,
        TRequest request)
    where TRequest : Framework.Mediator.Requests.IRequest<TRequest, TResponse>
    {
        return new RequestProcess<TRequest, TResponse>(request, processor);
    }

    public static IProcess<TResponse> Follow<TResponse>(
        this IProcess<TResponse> process,
        IProcess<TResponse> complementaryProcess)
    {
        return new ComplementaryProcess<TResponse>(process, complementaryProcess);
    }

    public static IProcess<TResponse> Follow<TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, IProcess<TResponse>> complementaryProcessFactory)
    {
        return new ComplementaryProcess<TResponse>(process, complementaryProcessFactory);
    }

    public static IProcess<TResponse> Follow<TComplementaryRequest, TResponse>(
        this IProcess<TResponse> process,
        TComplementaryRequest complementaryRequest,
        IRequestProcessor<TComplementaryRequest, TResponse> complementaryRequestProcessor)
    where TComplementaryRequest : Framework.Mediator.Requests.IRequest<TComplementaryRequest, TResponse>
    {
        var complementaryProcessFactory = delegate (Result<TResponse> result)
        {
            return new RequestProcess<TComplementaryRequest, TResponse>(complementaryRequest, complementaryRequestProcessor);
        };
        return new ComplementaryProcess<TResponse>(process, complementaryProcessFactory);
    }

    public static IProcess<TResponse> Follow<TComplementaryRequest, TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, TComplementaryRequest> complementaryRequestFactory,
        IRequestProcessor<TComplementaryRequest, TResponse> complementaryRequestProcessor)
    where TComplementaryRequest : Framework.Mediator.Requests.IRequest<TComplementaryRequest, TResponse>
    {
        var complementaryProcessFactory = delegate (Result<TResponse> result)
        {
            var complementaryRequest = complementaryRequestFactory(result);
            return new RequestProcess<TComplementaryRequest, TResponse>(complementaryRequest, complementaryRequestProcessor);
        };
        return new ComplementaryProcess<TResponse>(process, complementaryProcessFactory);
    }

    public static IProcess<TResponse> WithCompensator<TResponse>(
        this IProcess<TResponse> process,
        IProcess<TResponse> compensatorProcess)
    {
        return new CompensatorProcess<TResponse>(process, compensatorProcess);
    }

    public static IProcess<TResponse> WithCompensator<TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, IProcess<TResponse>> compensatorProcessFactory)
    {
        return new CompensatorProcess<TResponse>(process, compensatorProcessFactory);
    }

    public static IProcess<TResponse> WithCompensator<TCompensatorRequest, TResponse>(
        this IProcess<TResponse> process,
        TCompensatorRequest compensatorRequest,
        IRequestProcessor<TCompensatorRequest, TResponse> compensatorRequestProcessor)
    where TCompensatorRequest : Framework.Mediator.Requests.IRequest<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            return new RequestProcess<TCompensatorRequest, TResponse>(compensatorRequest, compensatorRequestProcessor);
        };
        return new CompensatorProcess<TResponse>(process, compensatorProcessFactory);
    }

    public static IProcess<TResponse> WithCompensator<TCompensatorRequest, TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, TCompensatorRequest> compensatorRequestFactory,
        IRequestProcessor<TCompensatorRequest, TResponse> compensatorRequestProcessor)
    where TCompensatorRequest : Framework.Mediator.Requests.IRequest<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            var compensatorRequest = compensatorRequestFactory(result);
            return new RequestProcess<TCompensatorRequest, TResponse>(compensatorRequest, compensatorRequestProcessor);
        };
        return new CompensatorProcess<TResponse>(process, compensatorProcessFactory);
    }
}