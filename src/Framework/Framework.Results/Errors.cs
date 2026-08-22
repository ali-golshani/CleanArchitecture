namespace Framework.Results;

public static class Errors
{
    public static readonly Error Unexpected = new(ErrorCodes.Unexpected, ErrorType.Unexpected, Resources.ErrorMessages.Unexpected);
    public static readonly Error Unauthorized = new(ErrorCodes.Unauthorized, ErrorType.Unauthorized, Resources.ErrorMessages.Unauthorized);
    public static readonly Error Timeout = new(ErrorCodes.Timeout, ErrorType.Timeout, Resources.ErrorMessages.Timeout);
    public static readonly Error OperationCanceled = new(ErrorCodes.OperationCanceled, ErrorType.Canceled, Resources.ErrorMessages.OperationCanceled);
    public static readonly Error NotSupported = new(ErrorCodes.NotSupported, ErrorType.NotSupported, Resources.ErrorMessages.NotSupported);
    public static readonly Error NotImplemented = new(ErrorCodes.NotImplemented, ErrorType.NotImplemented, Resources.ErrorMessages.NotImplemented);
    public static readonly Error NotFound = new(ErrorCodes.NotFound, ErrorType.NotFound, Resources.ErrorMessages.NotFound);
    public static readonly Error InvalidRequest = new(ErrorCodes.InvalidRequest, ErrorType.Validation, Resources.ErrorMessages.InvalidRequest);
    public static readonly Error Forbidden = new(ErrorCodes.Forbidden, ErrorType.Forbidden, Resources.ErrorMessages.Forbidden);
}
