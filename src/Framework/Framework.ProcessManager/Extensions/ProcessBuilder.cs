using Framework.Mediator.Requests;
using Framework.Results;

namespace Framework.ProcessManager.Extensions;

public static class ProcessBuilder
{
    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this IRequestProcessor<TRequest, TResponse> processor,
        TRequest request)
    where TRequest : IRequest<TRequest, TResponse>
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
    where TComplementaryRequest : IRequest<TComplementaryRequest, TResponse>
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
    where TComplementaryRequest : IRequest<TComplementaryRequest, TResponse>
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
    where TCompensatorRequest : IRequest<TCompensatorRequest, TResponse>
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
    where TCompensatorRequest : IRequest<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            var compensatorRequest = compensatorRequestFactory(result);
            return new RequestProcess<TCompensatorRequest, TResponse>(compensatorRequest, compensatorRequestProcessor);
        };
        return new CompensatorProcess<TResponse>(process, compensatorProcessFactory);
    }
}