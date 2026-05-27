namespace Framework.Results;

public static class Errors
{
    public static readonly Error Unexpected = new(ErrorType.Unexpected, Resources.ErrorMessages.Unexpected);
    public static readonly Error Unauthorized = new(ErrorType.Unauthorized, Resources.ErrorMessages.Unauthorized);
    public static readonly Error Timeout = new(ErrorType.Timeout, Resources.ErrorMessages.Timeout);
    public static readonly Error OperationCanceled = new(ErrorType.Canceled, Resources.ErrorMessages.OperationCanceled);
    public static readonly Error NotSupported = new(ErrorType.NotSupported, Resources.ErrorMessages.NotSupported);
    public static readonly Error NotImplemented = new(ErrorType.NotImplemented, Resources.ErrorMessages.NotImplemented);
    public static readonly Error NotFound = new(ErrorType.NotFound, Resources.ErrorMessages.NotFound);
    public static readonly Error InvalidRequest = new(ErrorType.Validation, Resources.ErrorMessages.InvalidRequest);
    public static readonly Error Forbidden = new(ErrorType.Forbidden, Resources.ErrorMessages.Forbidden);
}
