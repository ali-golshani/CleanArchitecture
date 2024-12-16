using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RequestAudit;

public sealed class RequestAuditMiddlewareBuilder
{
    private readonly RequestAuditAgent commandAudit;
    private readonly ILogger logger;

    public RequestAuditMiddlewareBuilder(RequestAuditAgent commandAudit, ILogger logger)
    {
        this.commandAudit = commandAudit;
        this.logger = logger;
    }

    public IMiddleware<TRequest, TResponse> Build<TRequest, TResponse>(string lggingDomain)
    where TRequest : Request
    {
        return new RequestAuditMiddleware<TRequest, TResponse>(commandAudit, lggingDomain, logger);
    }
}
