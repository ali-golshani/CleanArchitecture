namespace Framework.Exceptions;

public class RequestCanceledException(Exception innerException)
    : BaseSystemException(Resources.ExceptionMessages.RequestCanceledException, innerException)
{
    public override bool ShouldLog => true;
}