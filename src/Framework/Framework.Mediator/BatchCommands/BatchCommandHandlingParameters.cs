namespace Framework.Mediator.BatchCommands;

public class BatchCommandHandlingParameters
{
    public static readonly BatchCommandHandlingParameters Default = new BatchCommandHandlingParameters(continueOnErrors: false);
    public static readonly BatchCommandHandlingParameters Safe = new BatchCommandHandlingParameters(continueOnErrors: true);

    public BatchCommandHandlingParameters(bool continueOnErrors, TimeSpan? iterationDelay = null, TimeSpan? delayOnError = null)
    {
        ContinueOnErrors = continueOnErrors;
        IterationDelay = iterationDelay;
    }

    public bool ContinueOnErrors { get; }
    public TimeSpan? IterationDelay { get; }
    public TimeSpan? DelayOnError { get; }
}
