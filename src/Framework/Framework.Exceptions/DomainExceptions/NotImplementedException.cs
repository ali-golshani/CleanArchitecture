namespace Framework.Exceptions.DomainExceptions;

public class NotImplementedException : DomainException
{
    private const string DefaultMessage = "درخواست مورد نظر توسط سیستم پیاده سازی نشده است";

    public NotImplementedException(string message = DefaultMessage)
        : base(message)
    { }

    public override bool ShouldLog => true;
}
