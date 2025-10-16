namespace Framework.Domain.DomainEvents;

public enum DomainEventPublishStatus : sbyte
{
    InProcess = 0,
    Published = 1,
    Failed = -1,
}
