namespace Framework.Exceptions.DomainExceptions;

public class NotSupportedException(string message = NotSupportedException.DefaultMessage)
    : DomainException(message)
{
    private const string DefaultMessage = "درخواست مورد نظر توسط سیستم پشتیبانی نمی شود";

    public override bool ShouldLog => true;
}
