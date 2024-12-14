using Framework.Exceptions;
using Framework.Exceptions.Extensions;
using Framework.Results.Errors;
using Framework.Results.Exceptions;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ExceptionHandlingDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly ILogger logger;

    public ExceptionHandlingDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
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
            _ => exception.Messages.Select(x => new FailureError(x)).ToArray(),
        };
    }
}