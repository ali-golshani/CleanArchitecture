namespace CleanArchitecture.Mediator;

public interface IIntegrationEvent
{
    bool FireAndForget { get; }
}
