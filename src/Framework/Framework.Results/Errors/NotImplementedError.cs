namespace Framework.Results.Errors;

public class NotImplementedError(string message = ErrorMessages.NotImplemented)
    : Error(ErrorType.NotImplemented, message)
{
    public static readonly NotImplementedError Default = new();
}
