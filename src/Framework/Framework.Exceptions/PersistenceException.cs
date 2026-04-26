namespace Framework.Exceptions;

public abstract class PersistenceException(string message, Exception innerException) : BaseSystemException(message, innerException)
{
    public override bool ShouldLog => true;

    public PersistenceException(Exception innerException)
        : this(Resources.ExceptionMessages.PersistenceException, innerException)
    { }
}
