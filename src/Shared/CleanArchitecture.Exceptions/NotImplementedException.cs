using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class NotImplementedException(string? message = null)
    : DomainException(message ?? Resources.ExceptionMessages.NotImplemented)
{
    public override bool ShouldLog => true;
}