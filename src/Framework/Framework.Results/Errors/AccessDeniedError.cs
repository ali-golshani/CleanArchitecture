namespace Framework.Results.Errors;

public class AccessDeniedError() : Error(ErrorType.Forbidden, ErrorMessages.AccessDenied)
{
    public static readonly AccessDeniedError Default = new();
}
