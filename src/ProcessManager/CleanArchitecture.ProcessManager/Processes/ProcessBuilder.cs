using CleanArchitecture.Ordering.Commands;
using Framework.Exceptions;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal static class ProcessBuilder
{
    public static OrderingCommandProcess<TRequest, TResponse> Process<TRequest, TResponse>(
        this ICommandService commandService,
        ICommand<TRequest, TResponse> request)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var command = request as TRequest ?? throw new ProgrammerException();
        return new OrderingCommandProcess<TRequest, TResponse>(command, commandService);
    }

    public static ComplementaryProcess<TResponse> FollowedBy<TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, IProcess<TResponse>> complementaryProcessFactory)
    {
        return new ComplementaryProcess<TResponse>(process, complementaryProcessFactory);
    }

    public static CompensatorProcess<TResponse> WithCompensator<TResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, IProcess<TResponse>> compensatorProcessFactory)
    {
        return new CompensatorProcess<TResponse>(process, compensatorProcessFactory);
    }

    public static ComplementaryProcess<TResponse> Follow<TComplementaryRequest, TResponse>(
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

    public static ComplementaryProcess<TResponse> Follow<TComplementaryRequest, TResponse>(
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

    public static CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest, TResponse>(
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

    public static CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest, TResponse>(
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