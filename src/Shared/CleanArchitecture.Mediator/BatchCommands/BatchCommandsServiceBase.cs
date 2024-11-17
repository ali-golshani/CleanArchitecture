using CleanArchitecture.Mediator;
using Framework.Results.Exceptions;

namespace Framework.Mediator.BatchCommands;

public abstract class BatchCommandsServiceBase<TCommand>
    where TCommand : Command
{
    protected abstract Task<Result<Empty>> Handle(TCommand command, CancellationToken cancellationToken);

    public virtual async Task Handle(
        IReadOnlyCollection<TCommand> commands,
        BatchCommandHandlingParameters parameters,
        CancellationToken cancellationToken)
    {
        foreach (var command in commands)
        {
            await TryHandle(command, parameters.ContinueOnErrors, cancellationToken);
        }
    }

    private async Task TryHandle(TCommand command, bool continueOnErrors, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Handle(command, cancellationToken);

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
