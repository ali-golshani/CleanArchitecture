namespace Framework.Exceptions;

public class UnknownException(string message, Exception innerException) : BaseSystemException(message, innerException)
{
    public override bool IsFatal => true;
    public override bool ShouldLog => true;

    public UnknownException(Exception innerException)
        : this(Resources.ExceptionMessages.UnknownException, innerException)
    { }
}