namespace Framework.Exceptions.DomainExceptions;

public class InvalidRequestException(string message = ExceptionMessages.InvalidRequest)
    : DomainException(message)
{
}
