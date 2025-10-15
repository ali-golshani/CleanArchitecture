using Framework.Mediator.Notifications;

namespace CleanArchitecture.Ordering.Commands.Notifications.OrderRegistered;

internal sealed class Notification : INotification
{
    public required int OrderId { get; init; }
}
