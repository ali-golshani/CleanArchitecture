using Framework.Exceptions;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ExceptionHandlingDecorator<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
{
    private readonly IUseCase<TRequest, TResponse> next;
    private readonly ILogger logger;

    public ExceptionHandlingDecorator(
        IUseCase<TRequest, TResponse> next,
        ILogger logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
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