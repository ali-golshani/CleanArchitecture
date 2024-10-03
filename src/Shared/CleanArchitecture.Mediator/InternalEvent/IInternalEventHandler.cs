namespace CleanArchitecture.Mediator;

public interface IInternalEventHandler<in TEvent>
    where TEvent : IInternalEvent
{
    Task<Result<Empty>> Handle(TEvent @event);
}
