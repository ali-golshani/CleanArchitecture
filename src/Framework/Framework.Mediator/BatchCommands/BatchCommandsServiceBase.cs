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
            await DelayOnIteration(parameters, cancellationToken);
        }
    }

    private async Task TryHandle(
        TCommand command,
        BatchCommandHandlingParameters parameters,
        CancellationToken cancellationToken)
    {
        Result<Empty> result;

        try
        {
            result = await Handle(command, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception exp)
        {
            await OnError(command, exp);

            if (!parameters.ContinueOnErrors)
            {
                throw;
            }

            await DelayOnError(parameters, cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            return;
        }

        await OnError(command, result.Errors);

        if (!parameters.ContinueOnErrors)
        {
            throw new ErrorsException(result.Errors);
        }

        await DelayOnError(parameters, cancellationToken);
    }

    private static async Task DelayOnIteration(BatchCommandHandlingParameters parameters, CancellationToken cancellationToken)
    {
        if (parameters.IterationDelay > TimeSpan.Zero)
        {
            await Task.Delay(parameters.IterationDelay.Value, cancellationToken);
        }
    }

    private static async Task DelayOnError(BatchCommandHandlingParameters parameters, CancellationToken cancellationToken)
    {
        if (parameters.DelayOnError > TimeSpan.Zero)
        {
            await Task.Delay(parameters.DelayOnError.Value, cancellationToken);
        }
    }

    protected virtual ValueTask OnError(TCommand command, Error[] errors) => ValueTask.CompletedTask;
    protected virtual ValueTask OnError(TCommand command, Exception exp) => ValueTask.CompletedTask;
}
