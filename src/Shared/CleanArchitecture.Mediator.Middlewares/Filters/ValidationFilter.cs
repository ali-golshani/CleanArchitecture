using FluentValidation;
using Framework.Validation;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class ValidationFilter<TRequest, TResponse> :
    IFilter<TRequest, TResponse>
{
    private readonly IValidator<TRequest>[] validators;

    public ValidationFilter(IEnumerable<IValidator<TRequest>>? validators)
    {
        this.validators = validators?.ToArray() ?? [];
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IPipe<TRequest, TResponse> pipe)
    {
        var validationResult = await validators.ValidateAsync(context.Request);
        var errors = validationResult.Errors();

        if (errors.Length > 0)
        {
            return errors;
        }

        return await pipe.Send(context);
    }
}