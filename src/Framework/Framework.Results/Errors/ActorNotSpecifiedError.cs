namespace Framework.Results;

public class ActorNotSpecifiedError : Error
{
    public static readonly ActorNotSpecifiedError Default = new();

    public ActorNotSpecifiedError()
        : base(ErrorType.Unauthorized, ErrorMessages.ActorNotSpecified)
    { }
}
