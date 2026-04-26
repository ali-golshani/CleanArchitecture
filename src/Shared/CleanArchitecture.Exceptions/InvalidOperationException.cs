using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class InvalidOperationException(string? message = null)
    : DomainException(message ?? Resources.ExceptionMessages.InvalidOperation);