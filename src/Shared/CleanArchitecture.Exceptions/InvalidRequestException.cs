using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class InvalidRequestException(string? message = null)
    : DomainException(message ?? Resources.ExceptionMessages.InvalidRequest);