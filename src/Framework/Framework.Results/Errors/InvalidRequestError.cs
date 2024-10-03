namespace Framework.Results;

public class InvalidRequestError : Error
{
    public static readonly InvalidRequestError Default = new();

    public InvalidRequestError(string? message = null)
        : base(ErrorType.Validation, message ?? ErrorMessages.InvalidRequest)
    { }
}
