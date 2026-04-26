using FluentValidation;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.RegisterUser;

internal sealed partial class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Username)
            .Matches(Utilities.Validation.UsernameRegex())
            .MinimumLength(3)
            .MaximumLength(32);

        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(128);

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(128);

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .MaximumLength(32);
    }
}
