namespace Framework.Exceptions;

public abstract class ExternalException : BaseSystemException
{
    protected ExternalException(string message) : base(message) { }
    protected ExternalException(string message, Exception innerException) : base(message, innerException) { }
}
