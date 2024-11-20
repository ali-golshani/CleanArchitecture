namespace Framework.Exceptions;

public class RequestCanceledException : BaseSystemException
{
    public RequestCanceledException(Exception inner) : base(inner)
    { }

    public override string Message => "عملیات لغو گردید";

    public override bool ShouldLog => true;
}
