namespace Framework.Exceptions.DomainExceptions;

public class NotImplementedException(string message = NotImplementedException.DefaultMessage)
    : DomainException(message)
{
    private const string DefaultMessage = "درخواست مورد نظر توسط سیستم پیاده سازی نشده است";

    public override bool ShouldLog => true;
}
