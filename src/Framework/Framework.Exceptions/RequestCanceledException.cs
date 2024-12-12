namespace Framework.Exceptions;

public class RequestCanceledException : BaseSystemException
{
    public RequestCanceledException(Exception inner) : base(inner)
    { }

    public override bool ShouldLog => true;
    public override string Message => "عملیات لغو گردید";
}
