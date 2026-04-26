namespace Framework.Mediator.Notifications;

public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    Task<Result<Empty>> Handle(TNotification notification, CancellationToken cancellationToken);
}
