namespace CleanArchitecture.Mediator;

public abstract class BatchCommandHandlerBase<TCommand>
    where TCommand : Command
{
    protected abstract Task<Result<Empty>> Handle(TCommand command);

    public virtual async Task Handle(IReadOnlyCollection<TCommand> commands, BatchCommandHandlingParameters parameters)
    {
        foreach (var command in commands)
        {
            await TryHandle(command, parameters.ContinueOnErrors);
        }
    }

    private async Task TryHandle(TCommand command, bool continueOnErrors)
    {
        try
        {
            var result = await Handle(command);

            if (result.IsFailure)
            {
                OnError(command, result.Errors, result.CorrelationId);

                if (!continueOnErrors)
                {
                    throw new DomainErrorsException(result.Errors);
                }
            }
        }
        catch (Exception exp)
        {
            OnError(command, exp);

            if (!continueOnErrors)
            {
                throw;
            }
        }
    }

    protected virtual void OnError(TCommand command, Error[] errors, string? correlationId) { }
    protected virtual void OnError(TCommand command, Exception exp) { }
}
