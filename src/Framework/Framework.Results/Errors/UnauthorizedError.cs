namespace Framework.Results.Errors;

public class UnauthorizedError(string message, params ErrorSource[] sources) : Error(ErrorType.Unauthorized, message, sources)
{
    public static readonly UnauthorizedError Default = new(Resources.ErrorMessages.Unauthorized);
}
