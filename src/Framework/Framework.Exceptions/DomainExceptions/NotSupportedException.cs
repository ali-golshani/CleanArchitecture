namespace Framework.Exceptions.DomainExceptions;

public class NotSupportedException : DomainException
{
    private const string DefaultMessage = "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";

    public NotSupportedException(string message = DefaultMessage)
        : base(message)
    { }

    public override bool ShouldLog => true;
}
