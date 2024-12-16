using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit.Extensions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.RequestAudit;

internal sealed class RequestAuditMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
    where TRequest : Request
{
    private readonly RequestAuditAgent commandAudit;
    private readonly string lggingDomain;
    private readonly ILogger logger;

    public RequestAuditMiddleware(
        RequestAuditAgent commandAudit,
        string loggingDomain,
        ILogger logger)
    {
        this.commandAudit = commandAudit;
        lggingDomain = loggingDomain;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var actor = context.Actor;
        var request = context.Request;

        var logEntry = request.LogEntry(actor, lggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = lggingDomain,
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
                commandAudit.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                commandAudit.Post(logEntry);
            }


            logger.LogInformation("{Request}", request);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            commandAudit.Post(logEntry);

            throw;
        }
    }
}