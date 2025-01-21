using Framework.Mediator;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Querying.Pipeline;

internal sealed class RequestAuditMiddleware<TRequest, TResponse>
    : RequestAuditMiddlewareBase<TRequest, TResponse>
    where TRequest : Request
{
    public RequestAuditMiddleware(
        RequestAuditAgent requestAudit,
        ILogger<RequestAuditMiddleware<TRequest, TResponse>> logger)
        : base(requestAudit, logger)
    { }

    protected override string LggingDomain { get; } = nameof(Querying);
}
