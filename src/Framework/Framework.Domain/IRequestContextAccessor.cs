namespace Framework.Domain;

public interface IRequestContextAccessor
{
    Guid? CorrelationId { get; }
}
