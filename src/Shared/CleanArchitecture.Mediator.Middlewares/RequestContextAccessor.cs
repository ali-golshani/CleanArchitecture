using Framework.Mediator;
using Framework.Persistence.Interceptors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestContextAccessor : ICorrelationIdProvider
{
    public Guid? CorrelationId { get; private set; }

    public void SetContext(Request request)
    {
        CorrelationId = request.CorrelationId;
    }
}
