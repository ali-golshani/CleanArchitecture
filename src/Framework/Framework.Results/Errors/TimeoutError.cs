namespace Framework.Results.Errors;

public class TimeoutError()
    : Error(ErrorType.Timeout, Resources.ErrorMessages.Timeout)
{
    public static readonly TimeoutError Default = new();
}
