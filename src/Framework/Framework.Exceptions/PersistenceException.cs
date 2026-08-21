namespace Framework.Exceptions;

public abstract class PersistenceException(string message, Exception innerException) : BaseSystemException(message, innerException)
{
    public PersistenceException(Exception innerException)
        : this(Resources.ExceptionMessages.PersistenceException, innerException)
    { }
}
