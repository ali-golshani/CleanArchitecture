using Framework.Mediator;
using Infrastructure.RequestAudit.Extensions;
using Microsoft.Extensions.Logging;
using Minimal.Mediator.Middlewares;
using System.Diagnostics;

namespace Infrastructure.RequestAudit;

public class RequestAuditMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
    where TRequest : Request
{
    private readonly IActorResolver actorResolver;
    private readonly RequestAuditAgent requestAudit;
    private readonly ILogger logger;

    public RequestAuditMiddleware(
        IActorResolver actorResolver,
        RequestAuditAgent requestAudit,
        ILogger<RequestAuditMiddleware<TRequest, TResponse>> logger)
    {
        this.actorResolver = actorResolver;
        this.requestAudit = requestAudit;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        var request = context.Request;
        var actor = actorResolver.Actor;

        var logEntry = request.LogEntry(actor, request.LoggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = request.LoggingDomain,
            Command = typeof(TRequest).Name,
            request.CorrelationId,
            Actor = actor
        });

        try
        {
            var result = await next.Handle(context);
            timer.Stop();

            if (result.IsSuccess)
            {
                var response = result.Value;
                logEntry.Responsed(responseTime: timer.Elapsed, request: request, response: response);
                requestAudit.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                requestAudit.Post(logEntry);
            }


            logger.LogInformation("{Request}", request);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            requestAudit.Post(logEntry);

            throw;
        }
    }
}