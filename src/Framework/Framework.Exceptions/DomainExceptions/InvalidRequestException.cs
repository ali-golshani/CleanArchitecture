namespace Framework.Exceptions.DomainExceptions;

public class InvalidRequestException(string message = InvalidRequestException.DefaultMessage)
    : DomainException(message)
{
    private const string DefaultMessage = "درخواست نامعتبر";
}
