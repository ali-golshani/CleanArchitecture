using CleanArchitecture.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.OrderRegistered;

internal sealed class Handler : IInternalEventHandler<OrderRegisteredEvent>
{
    public async Task<Result<Empty>> Handle(OrderRegisteredEvent @event)
    {
        await Task.CompletedTask;
        return Empty.Value;
    }
}
