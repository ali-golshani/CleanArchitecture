using Framework.Results;

namespace Framework.ProcessManager.Extensions;

public static class ProcessBuilder
{
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
}