namespace Framework.Exceptions;

public class UnknownException(Exception innerException)
    : BaseSystemException(Resources.ExceptionMessages.UnknownException, innerException)
{
    public override bool IsFatal => true;
    public override bool ShouldLog => true;
}