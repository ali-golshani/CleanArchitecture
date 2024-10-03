namespace Framework.Exceptions;

public abstract class ExternalException : BaseSystemException
{
    protected ExternalException() { }
    protected ExternalException(string message) : base(message) { }
    protected ExternalException(Exception innerException) : base(innerException) { }

    public override bool ShouldLog => true;
    public virtual string TechnicalMessage => string.Empty;
}
