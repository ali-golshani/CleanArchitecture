namespace Framework.Mediator.Exceptions;

public class MissingRequestPipelineException<TRequest> : ProgrammerException
{
    public MissingRequestPipelineException()
        : base(ExceptionMessage(typeof(TRequest)))
    { }

    private static string ExceptionMessage(Type requestType)
    {
        return $"The Pipeline for '{requestType}' is not Registered.";
    }
}
