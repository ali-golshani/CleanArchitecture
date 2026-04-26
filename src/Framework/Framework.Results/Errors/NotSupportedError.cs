namespace Framework.Results.Errors;

public class NotSupportedError(string message, params ErrorSource[] sources) : Error(ErrorType.NotSupported, message, sources)
{
    public static readonly NotSupportedError Default = new(Resources.ErrorMessages.NotSupported);
}
