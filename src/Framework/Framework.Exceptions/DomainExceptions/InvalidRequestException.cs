namespace Framework.Exceptions;

public class InvalidRequestException : DomainException
{
    public InvalidRequestException(string? message = null)
    {
        SpecialMessage = message;
    }

    public string? SpecialMessage { get; }

    public override string Message => SpecialMessage ?? "درخواست نامعتبر";
}
