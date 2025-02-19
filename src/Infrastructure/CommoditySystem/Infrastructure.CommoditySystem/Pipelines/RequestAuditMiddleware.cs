using CleanArchitecture.Actors;
using Framework.Mediator;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CommoditySystem.Pipelines;

internal sealed class RequestAuditMiddleware<TRequest, TResponse>
    : RequestAuditMiddlewareBase<TRequest, TResponse>
    where TRequest : Request
{
    public RequestAuditMiddleware(
        IActorResolver actorResolver,
        RequestAuditAgent requestAudit,
        ILogger<RequestAuditMiddleware<TRequest, TResponse>> logger)
        : base(actorResolver, requestAudit, logger)
    { }

    protected override string LggingDomain { get; } = nameof(CommoditySystem);
}
