namespace Framework.Results;

public class AccessDeniedError : Error
{
    public static readonly AccessDeniedError Default = new();

    public AccessDeniedError()
        : base(ErrorType.Forbidden, ErrorMessages.AccessDenied)
    { }
}
