using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class InvalidOperationException(string message = ExceptionMessages.InvalidOperation)
    : DomainException(message);