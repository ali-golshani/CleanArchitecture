using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Mediator.Requests;

namespace CleanArchitecture.ProcessManager.Processors;

internal static class Extensions
{
    public static IRequestProcessor<TRequest, TResponse> AsProcessor<TRequest, TResponse>(
        this ICommandService commandService)
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return new OrderingCommandProcessor<TRequest, TResponse>(commandService);
    }

    public static IRequestProcessor<TRequest, TResponse> AsProcessor<TRequest, TResponse>(
        this IQueryService queryService)
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return new OrderingQueryProcessor<TRequest, TResponse>(queryService);
    }
}
