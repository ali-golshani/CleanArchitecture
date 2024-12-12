namespace Framework.Exceptions.DomainExceptions;

public class NotImplementedException(string message = ExceptionMessages.NotImplemented)
    : DomainException(message)
{
    public override bool ShouldLog => true;
}
