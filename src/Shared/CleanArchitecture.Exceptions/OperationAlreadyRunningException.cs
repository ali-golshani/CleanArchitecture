using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class OperationAlreadyRunningException(string operationName)
    : DomainException(Resources.ExceptionMessageBuilder.OperationAlreadyRunning(operationName))
{
    public string OperationName { get; } = operationName;
}