using Framework.Exceptions.Utilities;

namespace Framework.Exceptions.DomainExceptions;

public class NotSupportedException : DomainException
{
    public NotSupportedException(string? message = null)
        : base(message ?? ExceptionMessages.NotSupported)
    { }

    public override bool ShouldLog => true;
}
