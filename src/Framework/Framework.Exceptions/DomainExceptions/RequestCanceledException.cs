namespace Framework.Exceptions;

public class RequestCanceledException : DomainException
{
    public override string Message => "عملیات لغو گردید";
}
