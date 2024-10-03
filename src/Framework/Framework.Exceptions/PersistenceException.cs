namespace Framework.Exceptions;

public abstract class PersistenceException : BaseSystemException
{
    protected PersistenceException() { }
    protected PersistenceException(Exception innerException) : base(innerException) { }

    public override bool ShouldLog => true;

    public override string Message => "خطای پایگاه داده";
}
