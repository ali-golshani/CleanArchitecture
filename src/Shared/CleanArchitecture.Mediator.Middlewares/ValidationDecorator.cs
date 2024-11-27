using FluentValidation;
using Framework.Validation;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ValidationDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly IValidator<TRequest>[] validators;

    public ValidationDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        IEnumerable<IValidator<TRequest>>? validators)
    {
        this.next = next;
        this.validators = validators?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
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