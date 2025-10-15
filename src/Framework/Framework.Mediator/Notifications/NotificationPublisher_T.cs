namespace Framework.Mediator.Notifications;

internal sealed class NotificationPublisher<TNotification>
    where TNotification : INotification
{
    private readonly IEnumerable<INotificationHandler<TNotification>> handlers;

    public NotificationPublisher(IEnumerable<INotificationHandler<TNotification>> handlers)
    {
        this.handlers = handlers;
    }

    public async Task<Result<Empty>> Publish(TNotification notification, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers)
        {
            var result = await handler.Handle(notification, cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }
        }

        return Empty.Value;
    }
}