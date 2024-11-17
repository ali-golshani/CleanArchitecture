using Framework.Application;
using Framework.Mediator.IntegrationEvents;

namespace CleanArchitecture.Ordering.Application.Utilities;

internal static class IntegrationEventsPublisher
{
    public static async Task PublishEvents(IIntegrationEventOutbox eventsOutbox, IIntegrationEventBus eventBus, CancellationToken cancellationToken)
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
