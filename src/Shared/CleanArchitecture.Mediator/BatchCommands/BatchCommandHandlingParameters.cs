namespace CleanArchitecture.Mediator.BatchCommands;

public class BatchCommandHandlingParameters
{
    public static readonly BatchCommandHandlingParameters Default = new BatchCommandHandlingParameters(continueOnErrors: false);
    public static readonly BatchCommandHandlingParameters Safe = new BatchCommandHandlingParameters(continueOnErrors: true);

    public BatchCommandHandlingParameters(bool continueOnErrors)
    {
        ContinueOnErrors = continueOnErrors;
    }

    public bool ContinueOnErrors { get; }
}
