using CleanArchitecture.Configurations;
using Framework.Exceptions;
using Framework.Exceptions.Extensions;
using Framework.WebApi.Extensions;

namespace CleanArchitecture.WebApi.Shared.Middlewares;

public class DomainExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly IEnvironment environment;
    private readonly ILogger logger;

    public DomainExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(next);

        this.next = next;
        this.environment = environment;
        logger = loggerFactory.CreateLogger<DomainExceptionHandlingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);

            if (ShouldLog(context.Response?.StatusCode))
            {
                var connection = ConnectionInfo(context);
                logger.LogError(@"{@Status} {@Connection}", context.Response?.StatusCode, connection);
            }
        }
        catch (Exception exp)
        {
            exp = exp.UnwrapAll();

            switch (exp)
            {
                case UserFriendlyException:
                case BaseSystemException:
                case Exception when environment.DeploymentStage != DeploymentStage.Development:
                    {
                        logger.LogError(exp, "An unexpected error occurred: {Message}", exp.Message);
                        await WriteAsProblem(context, exp);
                        return;
                    }
            }

            throw;
        }
    }

    private static async Task WriteAsProblem(HttpContext context, Exception exp)
    {
        var problem = exp.AsProblemDetails();
        context.Response.Clear();
        context.Response.StatusCode = problem.Status!.Value;
        await context.Response.WriteAsJsonAsync(problem);
    }

    private static bool ShouldLog(int? status)
    {
        if (status is null)
        {
            return false;
        }

        switch (status)
        {
            case StatusCodes.Status100Continue:
            case StatusCodes.Status200OK:
            case StatusCodes.Status201Created:
            case StatusCodes.Status202Accepted:
            case StatusCodes.Status204NoContent:
            case StatusCodes.Status203NonAuthoritative:
            case StatusCodes.Status205ResetContent:
            case StatusCodes.Status206PartialContent:
            case StatusCodes.Status207MultiStatus:
            case StatusCodes.Status208AlreadyReported:
            case StatusCodes.Status226IMUsed:
            case StatusCodes.Status300MultipleChoices:
            case StatusCodes.Status301MovedPermanently:
            case StatusCodes.Status302Found:
            case StatusCodes.Status303SeeOther:
            case StatusCodes.Status304NotModified:
            case StatusCodes.Status305UseProxy:
            case StatusCodes.Status306SwitchProxy:
            case StatusCodes.Status307TemporaryRedirect:
            case StatusCodes.Status308PermanentRedirect:
            case StatusCodes.Status410Gone:
            case StatusCodes.Status101SwitchingProtocols:
            case StatusCodes.Status102Processing:
                return false;

            case StatusCodes.Status412PreconditionFailed:
            case StatusCodes.Status413RequestEntityTooLarge:
            //case StatusCodes.Status413PayloadTooLarge:
            case StatusCodes.Status414RequestUriTooLong:
            //case StatusCodes.Status414UriTooLong:
            case StatusCodes.Status415UnsupportedMediaType:
            case StatusCodes.Status416RequestedRangeNotSatisfiable:
            //case StatusCodes.Status416RangeNotSatisfiable:
            case StatusCodes.Status417ExpectationFailed:
            case StatusCodes.Status418ImATeapot:
            case StatusCodes.Status419AuthenticationTimeout:
            case StatusCodes.Status421MisdirectedRequest:
            case StatusCodes.Status422UnprocessableEntity:
            case StatusCodes.Status423Locked:
            case StatusCodes.Status424FailedDependency:
            case StatusCodes.Status426UpgradeRequired:
            case StatusCodes.Status428PreconditionRequired:
            case StatusCodes.Status429TooManyRequests:
            case StatusCodes.Status431RequestHeaderFieldsTooLarge:
            case StatusCodes.Status451UnavailableForLegalReasons:
            case StatusCodes.Status500InternalServerError:
            case StatusCodes.Status501NotImplemented:
            case StatusCodes.Status502BadGateway:
            case StatusCodes.Status503ServiceUnavailable:
            case StatusCodes.Status504GatewayTimeout:
            case StatusCodes.Status505HttpVersionNotsupported:
            case StatusCodes.Status506VariantAlsoNegotiates:
            case StatusCodes.Status507InsufficientStorage:
            case StatusCodes.Status508LoopDetected:
            case StatusCodes.Status411LengthRequired:
            case StatusCodes.Status510NotExtended:
            case StatusCodes.Status408RequestTimeout:
            case StatusCodes.Status400BadRequest:
            case StatusCodes.Status401Unauthorized:
            case StatusCodes.Status402PaymentRequired:
            case StatusCodes.Status403Forbidden:
            case StatusCodes.Status404NotFound:
            case StatusCodes.Status405MethodNotAllowed:
            case StatusCodes.Status406NotAcceptable:
            case StatusCodes.Status407ProxyAuthenticationRequired:
            case StatusCodes.Status409Conflict:
            case StatusCodes.Status511NetworkAuthenticationRequired:
                return true;

            default:
                return false;
        }
    }

    private static object ConnectionInfo(HttpContext context)
    {
        try
        {
            return new
            {
                ServerPort = context.Connection.LocalPort,
                RemoteAddress = context.Connection?.RemoteIpAddress?.MapToIPv4()?.ToString(),
                RequestPath = context.Request.Path.Value,
            };
        }
        catch
        {
            return new
            {
                ServerPort = 0,
                RemoteAddress = "?",
                RequestPath = "?"
            };
        }
    }
}
