using Framework.Domain;
using Framework.Mediator;

namespace Framework.Application;

public sealed class RequestContextAccessor : IRequestContextAccessor
{
    public Guid? CorrelationId { get; private set; }

    public void SetContext(Request request)
    {
        CorrelationId = request.CorrelationId;
    }
}
