using Framework.Results;
using System.Net;

namespace Framework.WebApi.Extensions;

public static class ErrorTypeExtensions
{
    public static HttpStatusCode AsHttpStatusCode(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => HttpStatusCode.BadRequest,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => HttpStatusCode.Forbidden,
            ErrorType.Timeout => HttpStatusCode.RequestTimeout,
            ErrorType.Locked => HttpStatusCode.Locked,
            ErrorType.Unavailable => HttpStatusCode.ServiceUnavailable,
            ErrorType.NotSupported or ErrorType.NotImplemented => HttpStatusCode.NotImplemented,
            ErrorType.Canceled => HttpStatusCode.RequestTimeout,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
