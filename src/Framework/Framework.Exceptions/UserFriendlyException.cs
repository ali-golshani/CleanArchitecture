namespace Framework.Exceptions;

public class UserFriendlyException : Exception
{
    public string? CorrelationId { get; }
    public bool IsRegistered { get; }

    public virtual IReadOnlyCollection<string> Messages => [Message];

    internal protected UserFriendlyException(string message, string? correlationId = null, bool isRegistered = false)
        : base(message)
    {
        CorrelationId = correlationId;
        IsRegistered = isRegistered;
    }

    internal protected UserFriendlyException(Exception innerException, string message, string? correlationId = null, bool isRegistered = false)
        : base(message, innerException)
    {
        CorrelationId = correlationId;
        IsRegistered = isRegistered;
    }
}
