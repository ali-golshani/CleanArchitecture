using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.Notifications;

internal sealed class NotificationPublisher : INotificationPublisher
{
    private readonly IServiceProvider serviceProvider;

    public NotificationPublisher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<Empty>> Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        where TNotification : INotification
    {
        return
            serviceProvider
            .GetRequiredService<NotificationPublisher<TNotification>>()
            .Publish(notification, cancellationToken);
    }
}