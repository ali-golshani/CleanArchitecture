namespace Framework.Mediator;

public interface ICorrelationIdAccessor
{
    Guid? CorrelationId { get; }
}
