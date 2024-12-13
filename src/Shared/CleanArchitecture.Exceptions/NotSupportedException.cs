using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class NotSupportedException(string message = ExceptionMessages.NotSupported)
    : DomainException(message)
{
    public override bool ShouldLog => true;
}