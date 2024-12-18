namespace Framework.Exceptions;

public class UserFriendlyException : Exception
{
    public string? TraceId { get; }
    public bool IsRegistered { get; }
    public virtual IReadOnlyCollection<string> Messages => [Message];

    protected internal UserFriendlyException(string message, string? traceId = null, bool isRegistered = false)
        : base(message)
    {
        TraceId = traceId;
        IsRegistered = isRegistered;
    }

    protected internal UserFriendlyException(Exception innerException, string message, string? traceId = null, bool isRegistered = false)
        : base(message, innerException)
    {
        TraceId = traceId;
        IsRegistered = isRegistered;
    }
}