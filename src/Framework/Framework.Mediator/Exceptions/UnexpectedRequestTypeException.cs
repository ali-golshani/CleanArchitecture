namespace Framework.Mediator.Exceptions;

public sealed class UnexpectedRequestTypeException<TRequest> : ProgrammerException
{
    public UnexpectedRequestTypeException(object request)
        : base(ExceptionMessage(typeof(TRequest), request.GetType()))
    { }

    private static string ExceptionMessage(Type expectedType, Type requestType)
    {
        return $"Unexpected request type encountered: '{requestType}'. Expected type: '{expectedType}'.";
    }
}
