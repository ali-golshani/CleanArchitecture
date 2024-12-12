namespace Framework.Results.Errors;

public class InvalidRequestError(string message = ErrorMessages.InvalidRequest)
    : Error(ErrorType.Validation, message)
{
    public static readonly InvalidRequestError Default = new();
}
