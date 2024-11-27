using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Exceptions;
using Framework.ProcessManager;

namespace CleanArchitecture.ProcessManager.Extensions;

internal static class Extensions
{
    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this ICommandService commandService,
        ICommand<TRequest, TResponse> command)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        var request = command as TRequest ?? throw new ProgrammerException();
        return new OrderingCommandProcess<TRequest, TResponse>
        (
            commandService,
            request
        );
    }

    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this IQueryService queryService,
        IQuery<TRequest, TResponse> query)
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var request = query as TRequest ?? throw new ProgrammerException();
        return new OrderingQueryProcess<TRequest, TResponse>
        (
            queryService,
            request
        );
    }
}
