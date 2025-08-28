namespace Framework.Results.Errors;

public class ForbiddenError(string? message = null) 
    : Error(ErrorType.Forbidden, message ?? Resources.ErrorMessages.Forbidden)
{
    public static readonly ForbiddenError Default = new ();
}
