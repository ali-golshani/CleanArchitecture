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
}