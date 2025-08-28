namespace Framework.Results.Errors;

public class OperationCanceledError() 
    : Error(ErrorType.Canceled, Resources.ErrorMessages.OperationCanceled)
{
    public static readonly OperationCanceledError Default = new();
}
