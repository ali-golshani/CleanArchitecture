using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class OperationAlreadyRunningException : DomainException
{
    public OperationAlreadyRunningException(string operationName)
        : base(ExceptionMessages.OperationAlreadyRunning(operationName))
    {
        OperationName = operationName;
    }

    public string OperationName { get; }
}
