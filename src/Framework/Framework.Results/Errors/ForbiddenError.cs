namespace Framework.Results.Errors;

public class ForbiddenError(string message, params ErrorSource[] sources) : Error(ErrorType.Forbidden, message, sources)
{
    public static readonly ForbiddenError Default = new(Resources.ErrorMessages.Forbidden);
}
