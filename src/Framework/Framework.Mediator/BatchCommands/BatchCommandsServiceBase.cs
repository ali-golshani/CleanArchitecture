using Framework.Results.Exceptions;

namespace Framework.Mediator.BatchCommands;

public abstract class BatchCommandsServiceBase<TCommand>
{
    protected abstract Task<Result<Empty>> Handle(TCommand command, CancellationToken cancellationToken);

    public virtual async Task Handle(
        IReadOnlyCollection<TCommand> commands,
        BatchCommandHandlingParameters parameters,
        CancellationToken cancellationToken)
    {
        foreach (var command in commands)
        {
            await TryHandle(command, parameters, cancellationToken);
        }
    }

    private async Task TryHandle(TCommand command, BatchCommandHandlingParameters parameters, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Handle(command, cancellationToken);

            if (result.IsFailure)
            {
                await OnError(command, result.Errors, result.CorrelationId);

                if (!parameters.ContinueOnErrors)
                {
                    throw new DomainErrorsException(result.Errors);
                }
            }
        }
        catch (Exception exp)
        {
            await OnError(command, exp);

            if (!parameters.ContinueOnErrors)
            {
                throw;
            }
            else if (parameters.DelayOnError > TimeSpan.Zero)
            {
                await Task.Delay(parameters.DelayOnError.Value, cancellationToken);
            }
        }
        finally
        {
            if (parameters.IterationDelay > TimeSpan.Zero)
            {
                await Task.Delay(parameters.IterationDelay.Value, cancellationToken);
            }
        }
    }

    protected virtual ValueTask OnError(TCommand command, Error[] errors, string? correlationId) => ValueTask.CompletedTask;
    protected virtual ValueTask OnError(TCommand command, Exception exp) => ValueTask.CompletedTask;
}
