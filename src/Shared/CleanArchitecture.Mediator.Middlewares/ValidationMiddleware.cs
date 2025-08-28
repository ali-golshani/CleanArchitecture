using FluentValidation;
using Framework.Validation;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ValidationMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
{
    private readonly IValidator<TRequest>[] validators;

    public ValidationMiddleware(IEnumerable<IValidator<TRequest>>? validators)
    {
        this.validators = validators?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        var validationResult = await validators.ValidateAsync(context.Request);
        var errors = validationResult.Errors();

        if (errors.Length > 0)
        {
            return errors;
        }

        return await next.Handle(context);
    }
}