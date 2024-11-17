namespace Framework.Results;

public class NotSupportedError : Error
{
    public static readonly NotSupportedError Default = new();

    public NotSupportedError(string? message = null)
        : base(ErrorType.NotSupported, message ?? ErrorMessages.NotSupported)
    { }
}
