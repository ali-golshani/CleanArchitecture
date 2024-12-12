using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class InvalidRequestException(string message = ExceptionMessages.InvalidRequest)
    : DomainException(message)
{
}
