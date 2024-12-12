namespace Framework.Results;

public class UnauthorizionError : Error
{
    public static readonly UnauthorizionError Default = new();

    public UnauthorizionError()
        : base(ErrorType.Unauthorized, ErrorMessages.NotUnauthorized)
    { }
}
