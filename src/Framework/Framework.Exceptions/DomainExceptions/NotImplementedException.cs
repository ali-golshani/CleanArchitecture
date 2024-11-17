namespace Framework.Exceptions;

public class NotImplementedException : DomainException
{
    public NotImplementedException(string? message = null)
    {
        SpecialMessage = message;
    }

    public string? SpecialMessage { get; }

    public override bool ShouldLog => true;
    public override string Message => SpecialMessage ?? "درخواست مورد نظر توسط سیستم پیاده سازی نشده است";
}
