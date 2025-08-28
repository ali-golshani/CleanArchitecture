namespace Framework.Results.Errors;

public class UnexpectedError(string message, params ErrorSource[] sources) : Error(ErrorType.Unexpected, message, sources)
{
    public static readonly UnexpectedError Default = new(Resources.ErrorMessages.Unexpected);
}
