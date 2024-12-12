namespace Framework.Results.Errors;

public class UnexpectedError(string message = ErrorMessages.Unexpected)
    : Error(ErrorType.Unexpected, message)
{
    public static readonly UnexpectedError Default = new();
}
