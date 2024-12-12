namespace Framework.Results.Errors;

public class ForbiddenError(string message = ErrorMessages.Forbidden) : Error(ErrorType.Forbidden, message)
{
    public static readonly ForbiddenError Default = new ();
}
