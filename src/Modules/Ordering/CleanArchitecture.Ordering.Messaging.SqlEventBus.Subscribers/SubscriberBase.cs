using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using Framework.Mediator;
using Framework.Results.Extensions;

namespace CleanArchitecture.Ordering.Messaging.SqlEventBus.Subscribers;

public abstract class SubscriberBase
{
    protected readonly Actor actor;
    protected readonly ICommandService commandService;

    protected SubscriberBase(ICommandService commandService)
    {
        this.commandService = commandService;
        actor = new InternalServiceActor(GetType().Name);
    }

    protected async Task Handle<TRequest, TResponse>(
        ICommand<TRequest, TResponse> command,
        CancellationToken cancellationToken,
        RequestExecutionOptions? options = null)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        await commandService.Handle(actor, command, cancellationToken, options).ThrowIfFailure();
    }
}
