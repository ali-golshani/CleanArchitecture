namespace Framework.Results.Errors;

public class NotImplementedError(string? message = null)
    : Error(ErrorType.NotImplemented, message ?? Resources.ErrorMessages.NotImplemented)
{
    public static readonly NotImplementedError Default = new();
}
