namespace Framework.Mediator.Exceptions;

public class MultipleRequestPipelinesAreRegisteredException<TRequest> : ProgrammerException
{
    public MultipleRequestPipelinesAreRegisteredException()
        : base(ExceptionMessage(typeof(TRequest)))
    { }

    private static string ExceptionMessage(Type requestType)
    {
        return $"Multiple Pipelines of '{requestType}' is Registered.";
    }
}
