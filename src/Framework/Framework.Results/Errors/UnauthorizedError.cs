namespace Framework.Results.Errors;

public class UnauthorizedError() : Error(ErrorType.Unauthorized, ErrorMessages.Unauthorized)
{
    public static readonly UnauthorizedError Default = new();
}
