using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Mediator.Extensions;
using Framework.ProcessManager;

namespace CleanArchitecture.ProcessManager.Extensions;

internal static class Extensions
{
    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this ICommandService commandService,
        ICommand<TRequest, TResponse> command)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return new OrderingCommandProcess<TRequest, TResponse>
        (
            commandService,
            command.AsRequestType()
        );
    }

    public static IProcess<TResponse> Process<TRequest, TResponse>(
        this IQueryService queryService,
        IQuery<TRequest, TResponse> query)
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return new OrderingQueryProcess<TRequest, TResponse>
        (
            queryService,
            query.AsRequestType()
        );
    }
}
