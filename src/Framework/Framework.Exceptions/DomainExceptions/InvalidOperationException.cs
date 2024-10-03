namespace Framework.Exceptions;

public class InvalidOperationException : DomainException
{
    public InvalidOperationException(string? message = null)
    {
        SpecialMessage = message;
    }

    public string? SpecialMessage { get; }

    public override string Message => SpecialMessage ?? "عملیات نامعتبر";
}
