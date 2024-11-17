namespace Framework.Results;

public class UnknownError : Error
{
    public static readonly UnknownError Default = new ();

    public UnknownError(string? message = null)
        : base(ErrorType.Unexpected, message ?? ErrorMessages.UnknownError)
    { }
}
