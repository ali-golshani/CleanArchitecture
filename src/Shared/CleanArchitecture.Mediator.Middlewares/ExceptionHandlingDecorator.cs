using Framework.Exceptions;
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
            return new FailureError(systemException.Message);
        }
    }
}