namespace Framework.Mediator.DomainEvents;

public interface IDomainEvent
{
    public string Topic { get; }
    bool FireAndForget { get; }
}
