namespace Framework.Results.Errors;

public class InvalidRequestError(string message, params ErrorSource[] sources) : Error(ErrorType.Validation, message, sources)
{
    public static readonly InvalidRequestError Default = new(Resources.ErrorMessages.InvalidRequest);
}
