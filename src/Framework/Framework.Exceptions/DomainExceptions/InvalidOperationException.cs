using Framework.Exceptions.Utilities;

namespace Framework.Exceptions.DomainExceptions;

public class InvalidOperationException : DomainException
{
    public InvalidOperationException(string? message = null)
        : base(message ?? ExceptionMessages.InvalidOperation)
    { }
}
