using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class NotSupportedException(string? message = null)
    : DomainException(message ?? Resources.ExceptionMessages.NotSupported)
{
    public override bool ShouldLog => true;
}