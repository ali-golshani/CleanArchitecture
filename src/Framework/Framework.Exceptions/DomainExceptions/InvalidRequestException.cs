namespace Framework.Exceptions.DomainExceptions;

public class InvalidRequestException : DomainException
{
    private const string DefaultMessage = "درخواست نامعتبر";

    public InvalidRequestException(string message = DefaultMessage)
        : base(message)
    { }
}
