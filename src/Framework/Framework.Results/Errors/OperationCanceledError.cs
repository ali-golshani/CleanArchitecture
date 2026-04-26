namespace Framework.Results.Errors;

public class OperationCanceledError(string message, params ErrorSource[] sources) : Error(ErrorType.Canceled, message, sources)
{
    public static readonly OperationCanceledError Default = new(Resources.ErrorMessages.OperationCanceled);
}
