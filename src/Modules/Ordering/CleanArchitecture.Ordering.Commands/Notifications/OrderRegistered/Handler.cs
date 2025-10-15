using Framework.Mediator.Notifications;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Notifications.OrderRegistered;

internal sealed class Handler : INotificationHandler<Notification>
{
    public async Task<Result<Empty>> Handle(Notification notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Empty.Value;
    }
}
