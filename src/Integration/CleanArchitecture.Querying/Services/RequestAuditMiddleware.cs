using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Querying.Services;

internal sealed class RequestAuditMiddleware<TRequest, TResponse>
    : RequestAuditMiddlewareBase<TRequest, TResponse>
    where TRequest : Request
{
    public RequestAuditMiddleware(
        RequestAuditAgent commandAudit,
        ILogger<RequestAuditMiddleware<TRequest, TResponse>> logger)
        : base(commandAudit, logger)
    { }

    protected override string LggingDomain => nameof(Querying);
}
