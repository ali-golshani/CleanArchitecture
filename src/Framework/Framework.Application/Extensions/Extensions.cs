using Framework.Mediator.DomainEvents;

namespace Framework.Application.Extensions;

public static class Extensions
{
    public static async Task PublishEvents(this IDomainEventOutbox eventsOutbox, IDomainEventBus eventBus, CancellationToken cancellationToken)
    {
        var events = eventBus.Events;

        if (events.Count == 0)
        {
            return;
        }

        var groups = events.GroupBy(x => x.Topic);
        foreach (var group in groups)
        {
            await eventsOutbox.Publish(group.ToList(), group.Key, cancellationToken);
        }
    }
}
