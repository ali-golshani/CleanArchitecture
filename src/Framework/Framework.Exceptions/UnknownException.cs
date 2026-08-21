namespace Framework.Exceptions;

public class UnknownException(string message, Exception innerException) : BaseSystemException(message, innerException)
{
    public UnknownException(Exception innerException)
        : this(Resources.ExceptionMessages.UnknownException, innerException)
    { }
}