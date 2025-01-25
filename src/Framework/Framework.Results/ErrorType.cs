namespace Framework.Results;

public enum ErrorType
{
    Failure = 1,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden,
    Timeout,
    Locked,
    Unavailable,
    NotSupported,
    NotImplemented,
    Canceled,
}
