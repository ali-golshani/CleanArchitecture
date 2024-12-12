using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class OperationAlreadyRunningException(string operationName)
    : DomainException(ExceptionMessages.OperationAlreadyRunning(operationName))
{
    public string OperationName { get; } = operationName;
}