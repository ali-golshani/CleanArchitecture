using FluentValidation;
using FluentValidation.Results;
using Framework.Results;

namespace Framework.Validation;

public static class Extensions
{
    public static ValidationResult Validate<T>(this IValidator<T>[] validators, T value)
    {
        return new ValidationResult(validators.Select(x => x.Validate(value)));
    }

    public static async Task<ValidationResult> ValidateAsync<T>(this IValidator<T>[] validators, T value)
    {
        var results = new List<ValidationResult>();

        foreach (var validator in validators)
        {
            results.Add(await validator.ValidateAsync(value));
        }

        return new ValidationResult(results);
    }

    public static Error[] Errors(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
        {
            return [];
        }

        return
            [.. validationResult.Errors.Select(ToError)];
    }

    private static Error ToError(ValidationFailure failure)
    {
        return new Error
        (
            type: ErrorType.Validation,
            message: failure.ErrorMessage,
            sources: new ErrorSource(failure.PropertyName, failure.AttemptedValue)
        );
    }
}
