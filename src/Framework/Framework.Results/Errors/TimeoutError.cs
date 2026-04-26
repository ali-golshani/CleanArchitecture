namespace Framework.Results.Errors;

public class TimeoutError(string message, params ErrorSource[] sources) : Error(ErrorType.Timeout, message, sources)
{
    public static readonly TimeoutError Default = new(Resources.ErrorMessages.Timeout);
}
