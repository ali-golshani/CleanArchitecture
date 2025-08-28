namespace Framework.Exceptions;

public abstract class PersistenceException(Exception innerException)
    : BaseSystemException(Resources.ExceptionMessages.PersistenceException, innerException)
{
    public override bool ShouldLog => true;
}
