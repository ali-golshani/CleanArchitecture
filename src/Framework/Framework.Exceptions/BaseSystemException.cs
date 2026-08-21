using Framework.Exceptions.Utilities;

namespace Framework.Exceptions;

public abstract class BaseSystemException : Exception
{
    public string TraceId { get; } = SmallGuid.GetUniqueKey();
    public virtual IReadOnlyCollection<string> Messages => [Message];

    private protected BaseSystemException() { }
    private protected BaseSystemException(string message) : base(message) { }
    private protected BaseSystemException(string message, Exception innerException) : base(message, innerException) { }

    public virtual IEnumerable<Fact> Facts => [];
}
