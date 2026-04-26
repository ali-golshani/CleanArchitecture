using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.Notifications;

internal sealed class NotificationPublisher(IServiceProvider serviceProvider) : INotificationPublisher
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<Empty>> Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        where TNotification : INotification
    {
        return
            serviceProvider
            .GetRequiredService<NotificationPublisher<TNotification>>()
            .Publish(notification, cancellationToken);
    }
}