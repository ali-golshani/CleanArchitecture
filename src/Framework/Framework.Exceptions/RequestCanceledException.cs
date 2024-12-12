namespace Framework.Exceptions;

public class RequestCanceledException(Exception innerException)
    : BaseSystemException(ExceptionMessages.RequestCanceledException, innerException)
{
    public override bool ShouldLog => true;
}