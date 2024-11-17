using FluentValidation;
using Framework.Validation;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ValidationDecorator<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
{
    private readonly IUseCase<TRequest, TResponse> next;
    private readonly IValidator<TRequest>[] validators;

    public ValidationDecorator(
        IUseCase<TRequest, TResponse> next,
        IEnumerable<IValidator<TRequest>>? validators)
    {
        this.next = next;
        this.validators = validators?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
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