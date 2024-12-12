namespace Framework.Exceptions;

public class RequestCanceledException : BaseSystemException
{
    public RequestCanceledException(Exception innerException) : base(innerException)
    { }

    public override bool ShouldLog => true;
    public override string Message => "عملیات لغو گردید";
}
