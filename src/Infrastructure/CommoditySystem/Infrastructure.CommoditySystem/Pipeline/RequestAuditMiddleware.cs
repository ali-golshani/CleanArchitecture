using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CommoditySystem.Pipeline;

internal sealed class RequestAuditMiddleware<TRequest, TResponse>
    : RequestAuditMiddlewareBase<TRequest, TResponse>
    where TRequest : Request
{
    public RequestAuditMiddleware(
        RequestAuditAgent requestAudit,
        ILogger<RequestAuditMiddleware<TRequest, TResponse>> logger)
        : base(requestAudit, logger)
    { }

    protected override string LggingDomain { get; } = nameof(CommoditySystem);
}
