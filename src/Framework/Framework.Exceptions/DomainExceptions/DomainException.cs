namespace Framework.Exceptions;

public abstract class DomainException : BaseSystemException
{
    protected DomainException() { }
    protected DomainException(string message) : base(message) { }
    protected DomainException(Exception innerException) : base(innerException) { }
    protected DomainException(string message, Exception innerException) : base(message, innerException) { }

    public override bool ShouldLog => false;
}
