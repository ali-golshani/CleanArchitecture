namespace Framework.Results.Errors;

public class NotSupportedError(string message = ErrorMessages.NotSupported)
    : Error(ErrorType.NotSupported, message)
{
    public static readonly NotSupportedError Default = new();
}
