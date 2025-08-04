namespace Framework.Persistence.Interceptors;

public interface IRequestContextAccessor
{
    Guid? CorrelationId { get; }
}
