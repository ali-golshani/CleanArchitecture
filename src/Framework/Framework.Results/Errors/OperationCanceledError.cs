namespace Framework.Results;

public class OperationCanceledError : Error
{
    public static readonly OperationCanceledError Default = new();

    public OperationCanceledError()
        : base(ErrorType.Canceled, ErrorMessages.OperationCanceled)
    { }
}
