namespace CleanArchitecture.Mediator;

public interface IInternalEventPublisher
{
    Task<Result<Empty>> Publish<TEvent>(TEvent @event) where TEvent : IInternalEvent;
}