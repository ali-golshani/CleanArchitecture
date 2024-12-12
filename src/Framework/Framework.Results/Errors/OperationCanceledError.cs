namespace Framework.Results.Errors;

public class OperationCanceledError() : Error(ErrorType.Canceled, ErrorMessages.OperationCanceled)
{
    public static readonly OperationCanceledError Default = new();
}
