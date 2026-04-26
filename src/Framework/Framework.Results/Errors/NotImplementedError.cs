namespace Framework.Results.Errors;

public class NotImplementedError(string message, params ErrorSource[] sources) : Error(ErrorType.NotImplemented, message, sources)
{
    public static readonly NotImplementedError Default = new(Resources.ErrorMessages.NotImplemented);
}
