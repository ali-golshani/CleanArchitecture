using Framework.Exceptions;
using Framework.Exceptions.Extensions;
using Framework.Results.Errors;
using Framework.Results.Exceptions;
using Microsoft.Extensions.Logging;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ExceptionHandlingMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
{
    private readonly ILogger logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        try
        {
            return await next.Handle(context);
        }
        catch (Exception exp)
        {
            logger.LogError(exp, "{@Request} {@Error}", context.Request, exp);

            var systemException = exp.TranslateToSystemException();
            return Errors(systemException);
        }
    }

    private static Error[] Errors(BaseSystemException exception)
    {
        return exception switch
        {
            DomainErrorsException domainErrorsException => domainErrorsException.Errors,
            _ => [.. exception.Messages.Select(x => new FailureError(x))],
        };
    }
}