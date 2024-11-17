using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.MassTransit.Consumers;

public abstract class ConsumerBase
{
    protected readonly Actor actor;
    protected readonly ICommandService commandService;
    private readonly IQueryService queryService;

    protected ConsumerBase(ICommandService commandService, IQueryService queryService)
    {
        this.commandService = commandService;
        this.queryService = queryService;

        actor = new InternalServiceActor(GetType().Name);
    }

    protected async Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken) where TRequest :
        CommandBase,
        ICommand<TRequest, TResponse>
    {
        return await commandService.Handle(actor, command, cancellationToken);
    }

    protected async Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken) where TRequest :
        QueryBase,
        IQuery<TRequest, TResponse>
    {
        return await queryService.Handle(actor, query, cancellationToken);
    }
}
