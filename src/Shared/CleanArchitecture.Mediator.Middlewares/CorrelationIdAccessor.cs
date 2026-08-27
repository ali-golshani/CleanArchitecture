using Framework.Mediator;
using Framework.Persistence.Interceptors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class CorrelationIdAccessor : ICorrelationIdAccessor, ICorrelationIdProvider
{
    public Guid? CorrelationId { get; private set; }

    internal void Initialize(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
}
