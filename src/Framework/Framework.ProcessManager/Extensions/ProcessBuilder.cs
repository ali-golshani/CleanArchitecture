using Framework.Results;

namespace Framework.ProcessManager.Extensions;

public static class ProcessBuilder
{
    public static IProcess<TResponse> Follow<TFirstResponse, TResponse>(
        this IProcess<TFirstResponse> process,
        IProcess<TResponse> complementaryProcess)
    {
        return new ComplementaryProcess<TFirstResponse, TResponse>(process, complementaryProcess);
    }

    public static IProcess<TResponse> Follow<TFirstResponse, TResponse>(
        this IProcess<TFirstResponse> process,
        Func<Result<TFirstResponse>, IProcess<TResponse>> complementaryProcessFactory)
    {
        return new ComplementaryProcess<TFirstResponse, TResponse>(process, complementaryProcessFactory);
    }

    public static IProcess<TResponse> WithCompensator<TResponse, TCompensatorResponse>(
        this IProcess<TResponse> process,
        IProcess<TCompensatorResponse> compensatorProcess)
    {
        return new CompensatorProcess<TResponse, TCompensatorResponse>(process, compensatorProcess);
    }

    public static IProcess<TResponse> WithCompensator<TResponse, TCompensatorResponse>(
        this IProcess<TResponse> process,
        Func<Result<TResponse>, IProcess<TCompensatorResponse>> compensatorProcessFactory)
    {
        return new CompensatorProcess<TResponse, TCompensatorResponse>(process, compensatorProcessFactory);
    }
}