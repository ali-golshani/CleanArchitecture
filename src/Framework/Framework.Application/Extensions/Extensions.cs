using Framework.Mediator;
using Framework.Mediator.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application.Extensions;

public static class Extensions
{
    public static async Task PublishEvents(this IIntegrationEventOutbox eventsOutbox, IIntegrationEventBus eventBus, CancellationToken cancellationToken)
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

    public static void SetRequestContextAccessor(this IServiceProvider serviceProvider, Request request)
    {
        var contextAccessor = serviceProvider.GetRequiredService<RequestContextAccessor>();
        contextAccessor.SetContext(request);
    }
}
