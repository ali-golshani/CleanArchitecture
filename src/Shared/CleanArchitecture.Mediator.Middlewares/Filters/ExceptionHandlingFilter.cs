using Framework.Exceptions;
using Framework.Exceptions.Extensions;
using Framework.Results.Errors;
using Framework.Results.Exceptions;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ExceptionHandlingFilter<TRequest, TResponse> :
    IFilter<TRequest, TResponse>
{
    private readonly ILogger logger;

    public ExceptionHandlingFilter(ILogger logger)
    {
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IPipe<TRequest, TResponse> pipe)
    {
        try
        {
            return await pipe.Send(context);
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