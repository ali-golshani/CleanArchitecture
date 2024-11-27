using CleanArchitecture.Mediator.Middlewares;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchitecture.Audit;

public sealed class QueryAuditDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : Query
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly QueryAuditAgent queryAudit;
    private readonly string loggingDomain;
    private readonly ILogger logger;

    public QueryAuditDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        QueryAuditAgent queryAudit,
        string loggingDomain,
        ILogger logger)
    {
        this.next = next;
        this.queryAudit = queryAudit;
        this.loggingDomain = loggingDomain;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var actor = context.Actor;
        var request = context.Request;

        var logEntry = request.LogEntry(actor, loggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = loggingDomain,
            Query = typeof(TRequest).Name,
            request.CorrelationId,
            Actor = actor
        });

        try
        {
            var result = await next.Handle(context);
            timer.Stop();

            if (result.IsSuccess)
            {
                logEntry.Succeed(responseTime: timer.Elapsed);
                queryAudit.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                queryAudit.Post(logEntry);
            }

            logger.LogInformation("{Query}", request);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            queryAudit.Post(logEntry);

            throw;
        }
    }
}