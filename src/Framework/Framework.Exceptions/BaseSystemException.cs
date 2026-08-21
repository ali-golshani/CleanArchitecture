namespace Framework.Exceptions;

public abstract class BaseSystemException : Exception
{
    public string ErrorId { get; }

    private protected BaseSystemException(string message)
        : base(message)
    {
        ErrorId = CreateErrorId();
    }

    private protected BaseSystemException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
        ErrorId =
            innerException is BaseSystemException systemException
                ? systemException.ErrorId
                : CreateErrorId();
    }

    public virtual IEnumerable<Fact> Facts => [];
    public virtual IReadOnlyCollection<string> Messages => [Message];

    private static string CreateErrorId()
    {
        return Guid.NewGuid().ToString("N");
    }
}
