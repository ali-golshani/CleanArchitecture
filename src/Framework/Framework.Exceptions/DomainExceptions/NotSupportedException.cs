namespace Framework.Exceptions;

public class NotSupportedException : DomainException
{
    public NotSupportedException(string? message = null)
    {
        SpecialMessage = message;
    }

    public string? SpecialMessage { get; }

    public override bool ShouldLog => true;
    public override string Message => SpecialMessage ?? "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";
}
