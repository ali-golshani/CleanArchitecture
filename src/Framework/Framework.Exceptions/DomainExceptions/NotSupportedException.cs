namespace Framework.Exceptions.DomainExceptions;

public class NotSupportedException(string message = ExceptionMessages.NotSupported)
    : DomainException(message)
{
    public override bool ShouldLog => true;
}
