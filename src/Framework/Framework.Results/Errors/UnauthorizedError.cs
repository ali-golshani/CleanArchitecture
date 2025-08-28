namespace Framework.Results.Errors;

public class UnauthorizedError() 
    : Error(ErrorType.Unauthorized, Resources.ErrorMessages.Unauthorized)
{
    public static readonly UnauthorizedError Default = new();
}
