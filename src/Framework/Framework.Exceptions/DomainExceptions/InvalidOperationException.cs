namespace Framework.Exceptions.DomainExceptions;

public class InvalidOperationException(string message = ExceptionMessages.InvalidOperation)
    : DomainException(message)
{
}
