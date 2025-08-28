namespace Framework.Mediator.BatchCommands;

public class BatchCommandHandlingParameters
{
    public static readonly BatchCommandHandlingParameters Default = new(continueOnErrors: false);
    public static readonly BatchCommandHandlingParameters Safe = new(continueOnErrors: true);

    public BatchCommandHandlingParameters(bool continueOnErrors, TimeSpan? iterationDelay = null, TimeSpan? delayOnError = null)
    {
        ContinueOnErrors = continueOnErrors;
        IterationDelay = iterationDelay;
        DelayOnError = delayOnError;
    }

    public bool ContinueOnErrors { get; }
    public TimeSpan? IterationDelay { get; }
    public TimeSpan? DelayOnError { get; }
}
