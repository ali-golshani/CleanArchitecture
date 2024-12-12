namespace Framework.Exceptions.DomainExceptions;

public class OperationAlreadyRunningException : DomainException
{
    public OperationAlreadyRunningException(string operationName)
    {
        OperationName = operationName;
    }

    public override string Message => $"عملیات {OperationName} در حال اجرا و یا پایان یافته است";

    public string OperationName { get; }
}
