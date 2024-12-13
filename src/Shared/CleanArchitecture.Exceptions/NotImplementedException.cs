using Framework.Exceptions;

namespace CleanArchitecture.Exceptions;

public class NotImplementedException(string message = ExceptionMessages.NotImplemented)
    : DomainException(message)
{
    public override bool ShouldLog => true;
}