namespace Framework.Exceptions.DomainExceptions;

public class InvalidOperationException : DomainException
{
    private const string DefaultMessage = "عملیات نامعتبر";

    public InvalidOperationException(string message = DefaultMessage)
        : base(message)
    { }
}
