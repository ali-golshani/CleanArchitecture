namespace Framework.Exceptions.DomainExceptions;

public class InvalidOperationException(string message = InvalidOperationException.DefaultMessage)
    : DomainException(message)
{
    private const string DefaultMessage = "عملیات نامعتبر";
}
