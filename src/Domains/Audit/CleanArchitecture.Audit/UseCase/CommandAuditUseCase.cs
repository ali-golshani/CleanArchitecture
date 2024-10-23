using Framework.Results;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchitecture.Audit;

public abstract class CommandAuditUseCase<TRequest, TResponse>
    where TRequest : Command, IRequest<TRequest, TResponse>
{
    protected abstract string LoggingDomain { get; }
    protected abstract Task<Result<TResponse>> InternalExecute(TRequest request, Actor actor, CancellationToken cancellationToken);

    protected readonly ILogger logger;
    private readonly CommandAuditAgent commandLogger;

    protected CommandAuditUseCase(ILogger logger, CommandAuditAgent commandLogger)
    {
        this.logger = logger;
        this.commandLogger = commandLogger;
    }

    protected async Task<Result<TResponse>> Execute(TRequest command, Actor actor, CancellationToken cancellationToken)
    {
        var logEntry = command.LogEntry(actor, LoggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = LoggingDomain,
            Command = typeof(TRequest).Name,
            command.CorrelationId,
            Actor = actor
        });

        try
        {
            var result = await InternalExecute(command, actor, cancellationToken);
            timer.Stop();

            if (result.IsSuccess)
            {
                var response = result.Value;
                logEntry.Responsed(responseTime: timer.Elapsed, command: command, response: response);
                commandLogger.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                commandLogger.Post(logEntry);
            }


            logger.LogInformation("{Command}", command);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            commandLogger.Post(logEntry);

            throw;
        }
    }
}