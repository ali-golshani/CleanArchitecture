namespace Framework.Results.Errors;

public class UnknownError(string message = ErrorMessages.UnknownError)
    : Error(ErrorType.Unexpected, message)
{
    public static readonly UnknownError Default = new();
}
