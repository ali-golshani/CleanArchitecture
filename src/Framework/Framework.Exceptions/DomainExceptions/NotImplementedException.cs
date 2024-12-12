using Framework.Exceptions.Utilities;

namespace Framework.Exceptions.DomainExceptions;

public class NotImplementedException : DomainException
{
    public NotImplementedException(string? message = null)
        : base(message ?? ExceptionMessages.NotImplemented)
    { }

    public override bool ShouldLog => true;
}
