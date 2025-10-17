namespace Framework.Persistence.Interceptors;

public interface ICorrelationIdProvider
{
    Guid? CorrelationId { get; }
}
