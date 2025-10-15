namespace Framework.Mediator.Notifications;

public interface INotificationPublisher
{
    Task<Result<Empty>> Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        where TNotification : INotification;
}