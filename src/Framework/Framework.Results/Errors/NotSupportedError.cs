namespace Framework.Results.Errors;

public class NotSupportedError(string? message = null)
    : Error(ErrorType.NotSupported, message ?? Resources.ErrorMessages.NotSupported)
{
    public static readonly NotSupportedError Default = new();
}
