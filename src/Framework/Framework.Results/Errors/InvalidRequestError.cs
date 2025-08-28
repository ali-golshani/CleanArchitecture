namespace Framework.Results.Errors;

public class InvalidRequestError(string? message = null)
    : Error(ErrorType.Validation, message ?? Resources.ErrorMessages.InvalidRequest)
{
    public static readonly InvalidRequestError Default = new();
}
