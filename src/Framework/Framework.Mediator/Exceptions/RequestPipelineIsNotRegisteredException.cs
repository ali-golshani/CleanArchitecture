namespace Framework.Mediator.Exceptions;

public class RequestPipelineIsNotRegisteredException<TRequest> : ProgrammerException
{
    public RequestPipelineIsNotRegisteredException()
        : base(ExceptionMessage(typeof(TRequest)))
    { }

    private static string ExceptionMessage(Type requestType)
    {
        return $"Pipeline of '{requestType}' is not Registered.";
    }
}
