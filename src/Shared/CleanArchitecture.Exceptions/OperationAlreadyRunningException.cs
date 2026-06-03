using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class OperationAlreadyRunningException(string operationName)
    : DomainException(Resources.ExceptionMessageBuilder.OperationAlreadyRunning(operationName))
{
    public string OperationName { get; } = operationName;

    public override IEnumerable<Fact> Facts
    {
        get
        {
            yield return new(nameof(OperationName), OperationName);
        }
    }
}