namespace Framework.Exceptions;

public abstract class ExternalException : BaseSystemException
{
    protected ExternalException() { }
    protected ExternalException(string message) : base(message) { }
    protected ExternalException(Exception innerException) : base(innerException.Message, innerException) { }
    protected ExternalException(string message, Exception innerException) : base(message, innerException) { }

    public override bool ShouldLog => true;
}
