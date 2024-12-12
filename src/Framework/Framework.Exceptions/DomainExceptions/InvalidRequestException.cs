using Framework.Exceptions.Utilities;

namespace Framework.Exceptions.DomainExceptions;

public class InvalidRequestException : DomainException
{
    public InvalidRequestException(string? message = null)
        : base(message ?? ExceptionMessages.InvalidRequest)
    { }
}
