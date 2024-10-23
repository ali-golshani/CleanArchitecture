using Framework.Results;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchitecture.Audit;

public abstract class QueryAuditUseCase<TRequest, TResponse>
    where TRequest : Query, IRequest<TRequest, TResponse>
{
    protected abstract string LoggingDomain { get; }
    protected abstract Task<Result<TResponse>> InternalExecute(TRequest request, Actor actor, CancellationToken cancellationToken);

    protected readonly ILogger logger;
    private readonly QueryAuditAgent queryLogger;

    protected QueryAuditUseCase(ILogger logger, QueryAuditAgent queryLogger)
    {
        this.logger = logger;
        this.queryLogger = queryLogger;
    }

    protected async Task<Result<TResponse>> Execute(TRequest query, Actor actor, CancellationToken cancellationToken)
    {
        var logEntry = query.LogEntry(actor, LoggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = LoggingDomain,
            Query = typeof(TRequest).Name,
            query.CorrelationId,
            Actor = actor
        });

        try
        {
            var result = await InternalExecute(query, actor, cancellationToken);
            timer.Stop();

            if (result.IsSuccess)
            {
                logEntry.Succeed(responseTime: timer.Elapsed);
                queryLogger.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                queryLogger.Post(logEntry);
            }

            logger.LogInformation("{Query}", query);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            queryLogger.Post(logEntry);

            throw;
        }
    }
}