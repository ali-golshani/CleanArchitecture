namespace Framework.Exceptions;

public class RequestCanceledException(string message, Exception innerException) : BaseSystemException(message, innerException)
{
    public override bool ShouldLog => true;

    public RequestCanceledException(Exception innerException)
        : this(Resources.ExceptionMessages.RequestCanceledException, innerException)
    { }
}