namespace Framework.Results.Errors;

public class AuthorizionError() : Error(ErrorType.Unauthorized, ErrorMessages.Unauthorized)
{
    public static readonly AuthorizionError Default = new();
}
