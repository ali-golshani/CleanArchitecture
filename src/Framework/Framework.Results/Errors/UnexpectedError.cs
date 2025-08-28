namespace Framework.Results.Errors;

public class UnexpectedError(string? message = null)
    : Error(ErrorType.Unexpected, message ?? Resources.ErrorMessages.Unexpected)
{
    public static readonly UnexpectedError Default = new();
}
