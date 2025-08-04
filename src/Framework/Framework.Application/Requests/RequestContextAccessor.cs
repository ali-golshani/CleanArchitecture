using Framework.Mediator;
using Framework.Persistence.Interceptors;

namespace Framework.Application.Requests;

public sealed class RequestContextAccessor : IRequestContextAccessor
{
    public Guid? CorrelationId { get; private set; }

    public void SetContext(Request request)
    {
        CorrelationId = request.CorrelationId;
    }
}
